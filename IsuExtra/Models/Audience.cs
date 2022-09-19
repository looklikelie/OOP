using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Audience
    {
        public Audience(int name)
        {
            if (name is < 0 or > 499)
            {
                throw new IsuExtraException("Audience does not exist");
            }

            Name = name;
        }

        public int Name { get; private set; }
    }
}