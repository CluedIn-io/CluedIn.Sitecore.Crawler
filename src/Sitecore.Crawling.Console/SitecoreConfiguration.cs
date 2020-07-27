using CluedIn.Crawling.Sitecore.Core;
using System.Collections.Generic;

namespace CluedIn.CrawlerSitecore.Console
{
  public static class SitecoreConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
        {
          { SitecoreConstants.KeyName.CertData, "MIIDKjCCAhKgAwIBAgIQF7BSzpM5YpBM1VLgWmC1NjANBgkqhkiG9w0BAQUFADAaMRgwFgYDVQQDDA94Q29ubmVjdC5jbGllbnQwHhcNMTgwODE0MDc0MjA3WhcNMTkwODE0MDgwMjA3WjAaMRgwFgYDVQQDDA94Q29ubmVjdC5jbGllbnQwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC6/J7w6nxfnoUPtwZ5fzNdocQUrVXi3KXVq9ebTgnzP+iq5C56MDuQ9rR66zVr7OAt/7lNJ11gzdHnLH3tpZLVNNIL3LAnQt/p1zyj/tbG/48vunb7ALXlW2WI3Ph+z93Kon7vKdtzv3J0YteaS77YjguKtiFLEhreyp3L66t3+yeiU1hIMCuOLB0XW4RhYug9NhkFNtk09gkB5D78UO20mz6Uzk4beNWxnv9xv7h/VszgcObO8paAVMcQW8W3obcoxl7QvRaFTPn83jJYPZS4MOy0c+XENIM8BxiCCqxwsFxERgkWz/buxsb2SQz5HXPB7B/8l54JdB/eqxrMK9V9AgMBAAGjbDBqMA4GA1UdDwEB/wQEAwIFoDAdBgNVHSUEFjAUBggrBgEFBQcDAgYIKwYBBQUHAwEwGgYDVR0RBBMwEYIPeENvbm5lY3QuY2xpZW50MB0GA1UdDgQWBBQ815mpNARpQsluLuLTNM2d2OoZ5zANBgkqhkiG9w0BAQUFAAOCAQEALTcBKae/EQx++EKmUhIywL7Ib3ve89RMs97MT3s6cDUT7hECTiQl37oPtWROBR9g+PfYYeXFsd45Db2dnhqtZfxAFpJEFZvyboogmTawIF2qlecPx8uUgJsx1w0vyIc20rte4Oq6gXiVef7d7IUmEwLo6Azk2KkugFSfSf/6ZfQrFQNOlMvhysDOH7cN29pRQGu+Dz9sjXPU+DLRdJavbcLS3nobuULPv8RzUoJkMGRfCPd4qhUeOtlHPpxwLvF4CGXrXXzZpV/iW2swfIPEP90cmDv0GsiCgHIicPIL0dkBL854l0GTD7xaBY8pwcTM+16jlQ8UGRBf2d/THu8yQA==" },
          { SitecoreConstants.KeyName.Uri, "xconnect" }
        };
    }
  }
}
