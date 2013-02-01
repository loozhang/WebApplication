using System.Security.Principal;
using System.Web;

namespace WebApplication3
{
    public class PortalUser:GenericPrincipal
    {
        public new PortalIdentity Identity
        {
            get { return base.Identity as PortalIdentity; }
        }

        public PortalUser (PortalIdentity identity):base(identity,new string[0])
        {
        }

        public PortalUser(PortalIdentity identity, bool isSuperAdmin)
            : base(identity, new string[0])
        {
        }

        public static PortalUser GetUserPrincipal()
        {
            var userID = HttpContext.Current.User.Identity.Name;
            var identityUse = new PortalIdentity(userID);
            PortalUser user=new PortalUser(identityUse);
            return user;
        }
    }
}