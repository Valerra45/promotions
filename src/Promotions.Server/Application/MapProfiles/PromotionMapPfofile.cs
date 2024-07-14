using Application.Services.GoodsService;
using Application.Services.Managers;
using Application.Services.Orders;
using Application.Services.Partners;
using Application.Services.Pictures;
using Application.Services.Promotions;
using Application.Services.SendPromotions;
using AutoMapper;
using Domain.Promotions;

namespace Application.MapProfiles
{
    internal class PromotionMapProfile : Profile
    {
        public PromotionMapProfile()
        {
            CreateMap<Partner, PartnerResponse>()
                .ReverseMap();

            CreateMap<Partner, PartnerCreateOrEdit>()
                .ReverseMap();

            CreateMap<Manager, ManagerResponse>()
                .ReverseMap();

            CreateMap<Manager, ManagerCreateOrEdit>()
                .ReverseMap();

            CreateMap<Picture, PictureResponse>();

            CreateMap<Goods, GoodsCreateOrEdit>()
                .ReverseMap();

            CreateMap<Goods, GoodsResponse>()
                .ReverseMap();

            CreateMap<Promotion, PromotionCreateOrEdit>()
                .ReverseMap();

            CreateMap<PromotionGoods, PromotionGoodsCreateOrEdit>()
                .ReverseMap();

            CreateMap<Promotion, PromotionShotResponse>()
                .ReverseMap();

            CreateMap<Promotion, PromotionResponse>()
                .ReverseMap();

            CreateMap<PromotionGoods, PromotionGoodsResponse>()
                .ReverseMap();

            CreateMap<Order, OrderShotResponse>()
                .ReverseMap();

            CreateMap<OrderGoods, OrderGoodsCreateOrEdit>()
                .ReverseMap();

            CreateMap<OrderGoods, OrderGoodsResponse>()
                .ReverseMap();

            CreateMap<SendPromotion, SendPromotionCreateOrEdit>()
                .ReverseMap();

            CreateMap<SendPromotion, SendPromotionResponse>()
                .ReverseMap();

            CreateMap<SendPromotion, SendPromotionShotResponse>()
                .ReverseMap();
        }
    }
}
