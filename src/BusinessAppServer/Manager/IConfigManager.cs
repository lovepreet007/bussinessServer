using BusinessAppServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.Manager
{
    public interface IConfigManager
    {
        LoginResponseVm Login(LoginRequestVm loginRequest);
        ResponseResult SubmitOrder(OrderDetailVm _orderDetails);
        List<Filters> GetFilters();
        List<Medicines> GetFiltersValues(string filterValue);
        List<Medicines> Search(string filterId, string searchItem, bool fromOrder);
        List<OwnersDetails> FetchOwnersData();
        List<OwnersDetails> FetchUpdatedOwnersData(string orderId);
        List<Report> ReportGeneration(ReportVm reportVm);
        ResponseResult Registration(RegistrationVm registrationRequest);
        ResponseResult ForgotPassword(string email);
        ResponseResult ResetPassword(ResetPasswordVm resetVm);

        List<FoodDetails> GetFoodDetails();
    }
}
