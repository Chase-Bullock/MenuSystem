using PizzaButt.NewModels;
using System.Collections.Generic;

namespace PizzaButt.ViewModels
{
    public class ToppingsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<SystemReference> ToppingTypes { get; set; }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            ToppingsViewModel p = obj as ToppingsViewModel;
            if ((System.Object)p == null)
                return false;

            return (Id == p.Id) && (Name == p.Name);
        }

        public bool Equals(ToppingsViewModel p)
        {
            if ((object)p == null)
                return false;

            return (Id == p.Id) && (Name == p.Name);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }

        class ToppingsViewModelEqualityComparer : IEqualityComparer<ToppingsViewModel>
        {
            public bool Equals(ToppingsViewModel b1, ToppingsViewModel b2)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null || b2 == null)
                    return false;
                else if (b1.Id == b2.Id && b1.Name == b2.Name)
                    return true;
                else
                    return false;
            }
            public int GetHashCode(ToppingsViewModel bx)
            {
                int hCode = (int)bx.Id;
                return hCode.GetHashCode();
            }

        }
    }

}