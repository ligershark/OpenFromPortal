namespace LigerShark.OpenFromPortal
{
    using System;
    
    /// <summary>
    /// Helper class that exposes all GUIDs used across VS Package.
    /// </summary>
    internal sealed partial class PackageGuids
    {
        public const string guidOpenFromPortalPkgString = "4156516b-f6e6-40f2-aecb-ff99cded5f8a";
        public const string guidOpenFromPortalCmdSetString = "8708293c-eac7-424a-b883-71861e2e5ab5";
        public const string guidImagesString = "6bbcf411-1039-4eed-9f3a-c05c1342e7b1";
        public static Guid guidOpenFromPortalPkg = new Guid(guidOpenFromPortalPkgString);
        public static Guid guidOpenFromPortalCmdSet = new Guid(guidOpenFromPortalCmdSetString);
        public static Guid guidImages = new Guid(guidImagesString);
    }
    /// <summary>
    /// Helper class that encapsulates all CommandIDs uses across VS Package.
    /// </summary>
    internal sealed partial class PackageIds
    {
        public const int cmdidMyCommand = 0x0100;
        public const int bmpPic1 = 0x0001;
    }
}
