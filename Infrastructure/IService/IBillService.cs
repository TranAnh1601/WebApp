using Infrastructure.Commons;
using Infrastructure.Model.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IService
{
    public interface IBillService
    {
        Task<ResponseResult> CreateBill(BillCreateViewModel model);
    }

}
