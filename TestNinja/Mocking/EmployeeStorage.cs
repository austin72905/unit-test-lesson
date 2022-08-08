using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        void DeleteEmployee(int id);
    }
    public class EmployeeStorage: IEmployeeStorage
    {
        private EmployeeContext _db;

        public EmployeeStorage()
        {
            _db = new EmployeeContext();
        }

        /*
            這個類裡面的方法只對外部資源操作，不用對他做單元測試，因為未來可能會換框架，最後整合測試再測就好 
         
        */
        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null) return;
            
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            
            
        }
    }

}
