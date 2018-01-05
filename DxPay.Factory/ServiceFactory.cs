using DxPay.Services;
using DxPay.Services.Impl;
using DxPay.Services.Inter;

namespace DxPay.Factory
{
    /// <summary>
    /// 服务层生产工厂类
    /// </summary>
    public class ServiceFactory
    {
        /// <summary>
        /// 创建泛型实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        private static TService CreateService<TService>() where TService : class
        {
            var service = InstanceFactory.GetServiceInstance<TService>();
            return service;
        }

        /// <summary>
        /// 获取合作表单服务
        /// </summary>
        public static ICoCooperationApplicationService CoCooperationApplicationService
        {
            get
            {
                return CreateService<CoCooperationApplicationService>();
            }
        }

        /// <summary>
        /// 获取代理商表单服务
        /// </summary>
        public static ICoAgentService CoAgentService
        {
            get
            {
                return CreateService<CoAgentService>();
            }
        }

      /// <summary>
      /// 应用表
      /// </summary>
       public static IAppService AppService
        {
            get
            {
                return CreateService<AppService>();
            }
        }

        /// <summary>
        /// 开发者表
        /// </summary>
        public static IUserService UserService
        {
            get
            {
                return CreateService<UserService>();
            }
        }

        /// <summary>
        /// 报表--通道成本服务
        /// </summary>
        public static ICoSettlementChannelCostService CoSettlementChannelCostService
        {
            get { return CreateService<CoSettlementChannelCostService>(); }
        }
        /// <summary>
        /// 商务表
        /// </summary>
       public static ICoBusinessPersonnelService CoBusinessPersonnelService
        {
            get
            {
                return CreateService<CoBusinessPersonnelService>();
            }
        }

        /// <summary>
        /// 报表--开发者结算总揽服务
        /// </summary>
        public static ICoSettlementDeveloperOverviewService CoSettlementDeveloperOverviewService
        {
            get { return CreateService<CoSettlementDeveloperOverviewService>(); }
        }
        /// <summary>
        /// 订单表
        /// </summary>
        public static IOrderService OrderService
        {
            get
            {
                return CreateService<OrderService>();
            }
        }

        /// <summary>
        /// 报表--开发者-应用详情服务
        /// </summary>
        public static ICoSettlementDeveloperAppDetailsService CoSettlementDeveloperAppDetailsService
        {
            get { return CreateService<CoSettlementDeveloperAppDetailsService>(); }
        }

       
        /// <summary>
        /// 营收表
        /// </summary>
        public static IUserReportService UserReportService
        {
            get
            {
                return CreateService<UserReportService>();
            }
        }
        /// <summary>
        /// 流量趋势
        /// </summary>
        public static ITrendsService TrendsService
        {
            get
            {
                return CreateService<TrendsService>();
            }
        }
        /// <summary>
        /// 手机品牌
        /// </summary>
        public static IStatisticsService StatisticsService
        {
            get
            {
                return CreateService<StatisticsService>();
            }
        }
        /// <summary>
        /// 手机型号
        /// </summary>
        public static IModelnumbersService ModelnumbersService
        {
            get
            {
                return CreateService<ModelnumbersService>();
            }
        }
        /// <summary>
        /// 操作系统
        /// </summary>
        public static IOperatingsystemService OperatingsystemService
        {
            get
            {
                return CreateService<OperatingsystemService>();
            }
        }
        /// <summary>
        /// 分辨率
        /// </summary>
        public static IResolutionService ResolutionService
        {
            get
            {
                return CreateService<ResolutionService>();
            }
        }
        /// <summary>
        /// 网络
        /// </summary>
        public static INetworkService NetworkService
        {
            get
            {
                return CreateService<NetworkService>();
            }
        }
        /// <summary>
        /// 手机运营商统计
        /// </summary>
        public static IOperatorService OperatorService
        {
            get
            {
                return CreateService<OperatorService>();
            }
        }
        /// <summary>
        /// 省份统计
        /// </summary>
        public static IProvinceService ProvinceService
        {
            get
            {
                return CreateService<ProvinceService>();
            }
        }

        /// <summary>
        /// 每日应用汇总
        /// </summary>
        public static IAppCountService AppCountService
        {
            get
            {
                return CreateService<AppCountService>();
            }
        }
    }
}



