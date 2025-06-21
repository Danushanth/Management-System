using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Views;
using UnicomTicManagementSystem.Repositories;
using SchoolManageSystem.Data;

namespace UnicomTICManagementSystem
{
    internal static class Programs
    {
        
        [STAThread]
        static void Main()
        {
            

            DatabaseInitializer.CreateTables();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
