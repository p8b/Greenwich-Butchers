using GreenwichButchers.Models;

namespace GreenwichButchers.SystemClasses
{
    public class LoginCheck
    {
        public CookieEncryptM cookieEncryptM { get; private set; } = new CookieEncryptM();

        public LoginCheck(string HashCookieID, bool HashRenew)
        {
            // Check if a user is logged in
            // First check if the cookie containing login state exists
            if (HashCookieID != "")
            {
                bool ControlBool = false;
                if (HashRenew)
                {
                    ControlBool = cookieEncryptM.WriteRead(HashCookieID, "");
                }
                else
                {
                    ControlBool = cookieEncryptM.Read(HashCookieID);
                }
                HashCookieID = "";
                if (ControlBool)
                {
                    // Extract the login state from the cookie and convert it from Base64String to
                    // Login object
                    var user = (LoginM)new ObjectConvert().String64ToObject(cookieEncryptM.Base64Value);

                    // If the user is customer
                    if (user?.PersonType == "Customer")
                    {
                        // Set the ViewData to true
                        CUser.LoginStatus = true;
                        CUser.NavBar = "_Navbar.cshtml";
                        CUser.UserRole = user.PersonType;
                        CUser.UserID = user.ID;
                        return;
                    }

                    if (user?.PersonType == "Manager" || user?.PersonType == "Staff")
                    {
                        CUser.LoginStatus = true;
                        CUser.NavBar = "_NavbarEmployee.cshtml";
                        CUser.UserRole = user.PersonType;
                        CUser.UserID = user.ID;
                        return;
                    }
                }
            }
            // Reset CUser Properties
            CUser.LoginStatus = false;
            CUser.NavBar = "_Navbar.cshtml";
            CUser.UserRole = "";
            CUser.UserID = -1000;
        }
    }
}

public static class CUser
{
    public static bool LoginStatus { get; set; }
    public static string NavBar { get; set; } = "_Navbar.cshtml";
    public static string UserRole { get; set; } = "";
    public static int UserID { get; set; }
}
