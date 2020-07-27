using System;
using System.Linq;
using System.Collections.Generic;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System.Threading.Tasks;
using crawlerModel = CluedIn.Crawling.Sitecore.Core.Models;
using Nito.AsyncEx;
using AutoMapper;
using AutoMapper.QueryableExtensions;

public class ContactCursor : IDisposable
{
  private XConnectClient _client;
  private DateTime _startTime;
  private DateTime _endTime;
  private readonly IMapper _mapper;

  private IAsyncEntityBatchEnumerator<Contact> _cursor;
  private IAsyncEntityBatchEnumerator<Contact> Cursor =>
      _cursor ?? (
          _cursor = AsyncContext.Run(
              async () => await CreateContactCursor()
              )
      );

  public IEnumerable<crawlerModel.Contact> Current =>
        Cursor.Current.AsQueryable().ProjectTo<crawlerModel.Contact>();

    public long TotalCount => Cursor.TotalCount;

  internal ContactCursor(XConnectClient client, DateTime startTime, IMapper mapper) : this(client, startTime, DateTime.UtcNow, mapper)
  { }
  internal ContactCursor(XConnectClient client, DateTime startTime, DateTime endTime, IMapper mapper)
  {
    _client = client;
    _startTime = startTime;
    _endTime = endTime;
    _mapper = mapper;
  }

  public bool MoveNext() =>
       AsyncContext.Run(async () => await Cursor.MoveNext());

  private async Task<IAsyncEntityBatchEnumerator<Contact>> CreateContactCursor(int chunksize = 200)
  {
    return await _client.CreateContactEnumerator(new ContactExpandOptions(PersonalInformation.DefaultFacetKey)
    {
      Interactions = new RelatedInteractionsExpandOptions(IpInfo.DefaultFacetKey)
      {
        StartDateTime = _startTime,
        EndDateTime = _endTime
      }
    }, chunksize).ConfigureAwait(false);
  }

  public void Dispose()
  {
    _client.Dispose();
  }
}
