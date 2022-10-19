using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleWindow
{
    public interface IWindowHelper
    {
        void SetMainViewId(int mainViewId);

        Task OpenMainWindowAsync();

        Task CreateSecondaryViewAsync();
    }
}
