using System;

namespace Company.OpenFromPortal
{
    static class GuidList
    {
        public const string guidOpenFromPortalPkgString = "4156516b-f6e6-40f2-aecb-ff99cded5f8a";
        public const string guidOpenFromPortalCmdSetString = "8708293c-eac7-424a-b883-71861e2e5ab5";

        public static readonly Guid guidOpenFromPortalCmdSet = new Guid(guidOpenFromPortalCmdSetString);
    };

    static class PkgCmdIDList
    {
        public const uint cmdidMyCommand = 0x100;
    };
}