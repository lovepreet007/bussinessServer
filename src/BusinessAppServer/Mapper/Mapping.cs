using AutoMapper;
using BusinessAppServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.Mapper
{
    public class Mapping : Profile
    {


        public Mapping()
        {

            CreateMap<OrderDetailVm, OrderDetailDm>()
            .ForMember(opt => opt.orderId, x => x.MapFrom(d => d.orderId))
            .ForMember(opt => opt.city, x => x.MapFrom(d => d.address.city))
            .ForMember(opt => opt.name, x => x.MapFrom(d => d.address.name))
            .ForMember(opt => opt.verification, x => x.MapFrom(d => d.address.verification))
            .ForMember(opt => opt.phoneNumber, x => x.MapFrom(d => d.address.phoneNumber))
            .ForMember(opt => opt.pinCode, o => o.MapFrom(d => d.address.pinCode))
            .ForMember(opt => opt.state, o => o.MapFrom(d => d.address.state))
            .ForMember(opt => opt.street, o => o.MapFrom(d => d.address.street))
            .ForMember(opt => opt.landmark, o => o.MapFrom(d => d.address.landmark))
            .ForMember(opt => opt.filterId, o => o.MapFrom(d => d.productToBuy.filterId))
            .ForMember(opt => opt.medicineId, o => o.MapFrom(d => d.productToBuy.medicineId))
            .ForMember(opt => opt.medicineManufacturer, o => o.MapFrom(d => d.productToBuy.medicineManufacturer))
            .ForMember(opt => opt.medicineName, o => o.MapFrom(d => d.productToBuy.medicineName))
            .ForMember(opt => opt.paymentOption, o => o.MapFrom(d => d.payment))
            .ForMember(opt => opt.isActive, o => o.MapFrom(d => d.isActive))
            .ForMember(opt => opt.orderPlacedOn, o => o.MapFrom(d => d.orderPlacedOn))
            .ForMember(opt => opt.orderCompleted, o => o.MapFrom(d => d.orderCompleted))
            .ForMember(opt => opt.paymentOption, o => o.MapFrom(d => d.payment)).ReverseMap();

            CreateMap<ReportVm, ReportDm>()
                .ForMember(opt => opt.fromDateDm, x => x.MapFrom(d => d.fromDate))
                 .ForMember(opt => opt.toDateDm, x => x.MapFrom(d => d.toDate))
                  .ForMember(opt => opt.statusDm, x => x.MapFrom(d => d.status));

            CreateMap<LoginRequestVm, LoginRequestDm>()
                .ForMember(opt => opt.username, x => x.MapFrom(d => d.username))
                 .ForMember(opt => opt.password, x => x.MapFrom(d => d.password)).ReverseMap();
            CreateMap<LoginResponseDm, LoginResponseVm>()
                .ForMember(opt => opt.status, x => x.MapFrom(d => d.status))
                  .ForMember(opt => opt.message, x => x.MapFrom(d => d.message)).ReverseMap();

            CreateMap<RegistrationVm, RegistrationDm>()
               .ForMember(opt => opt.userNameDm, x => x.MapFrom(d => d.userName))
                 .ForMember(opt => opt.mobileNumberDm, x => x.MapFrom(d => d.mobileNumber))
                 .ForMember(opt => opt.emailDm, x => x.MapFrom(d => d.email))
                 .ForMember(opt => opt.newPasswordDm, x => x.MapFrom(d => d.newPassword))
                 .ForMember(opt => opt.genderDm, x => x.MapFrom(d => d.gender))
                 .ForMember(opt => opt.dobDm, x => x.MapFrom(d => d.dob)).ReverseMap();

            CreateMap<ResetPasswordVm, ResetPasswordDm>()
              .ForMember(opt => opt.emailAddressDm, x => x.MapFrom(d => d.emailAddressVm))
              .ForMember(opt => opt.newPasswordDm, x => x.MapFrom(d => d.newPasswordVm))
                .ForMember(opt => opt.oldPasswordDm, x => x.MapFrom(d => d.oldPasswordVm)).ReverseMap();
        }
        public static List<OwnersDetails> OwnerDetailsData(List<OrderDetailVm> _orderDetailVm)
        {
            List<OwnersDetails> _ownerDetails = new List<OwnersDetails>();
            _ownerDetails = _orderDetailVm.Select(item => new OwnersDetails()
            {
                orderId = item.orderId,
                name = item.address.name,
                phoneNumber = item.address.phoneNumber,
                pinCode = item.address.pinCode,
                city = item.address.city,
                street = item.address.street,
                state = item.address.state,
                verification = item.address.verification,
                landmark = item.address.landmark,
                medicineId = item.productToBuy.medicineId,
                medicineName = item.productToBuy.medicineName,
                medicineManufacturer = item.productToBuy.medicineManufacturer,
                filterId = item.productToBuy.filterId,
                payment = item.payment,
                isActive = item.isActive,
                orderCompleted = item.orderCompleted,
                orderPlacedOn = item.orderPlacedOn
            }).ToList();

            return _ownerDetails;
        }





        //public string payment { get; set; }
        //public bool isActive { get; set; }
        //public DateTime orderPlacedOn { get; set; }
        //public DateTime orderCompleted { get; set; }
    }
}
