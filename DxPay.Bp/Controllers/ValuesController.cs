using System;
using System.Linq;
using System.Web.Http;
using DxPay.Bp.Models;
using DxPay.Factory;
using DxPay.Services;
using DxPay.Services.Inter;
using JMP.MDL;

namespace DxPay.Bp.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ICoCooperationApplicationService _coCooperationApplicationService;
        private readonly ICoSettlementChannelCostService _coSettlementChannelCostService;
        private readonly ICoSettlementDeveloperOverviewService _coSettlementDeveloperOverviewService;
        private readonly ICoSettlementDeveloperAppDetailsService _coSettlementDeveloperAppDetailsService;
        public ValuesController()
        {
            _coCooperationApplicationService = ServiceFactory.CoCooperationApplicationService;
            _coSettlementChannelCostService = ServiceFactory.CoSettlementChannelCostService;
            _coSettlementDeveloperOverviewService = ServiceFactory.CoSettlementDeveloperOverviewService;
            _coSettlementDeveloperAppDetailsService = ServiceFactory.CoSettlementDeveloperAppDetailsService;
        }

        [HttpGet]
        [LoginCheckFilter(IsCheck = true, IsRole = true)]
        public IHttpActionResult Get(int id, int pageIndex, [FromUri]int pageSize)
        {
            var list = _coCooperationApplicationService.FindPagedList("Id DESC", "Id>" + id, null, pageIndex, pageSize);
            var gridModel = new DataSource<CoCooperationApplication>(list)
            {
                Data = list.Select(x => x)
            };

            return Ok(gridModel);
        }

        [HttpGet]
        public IHttpActionResult Add()
        {
            _coCooperationApplicationService.Insert(new CoCooperationApplication
            {
                CreatedOn = DateTime.Now,
                EmailAddress = "admin@example.com",
                Name = "admin",
                MobilePhone = "13100000000",
                QQ = "10010",
                State = 0,
                RequestContent = "数据写入测试"
            });
            return Ok("success");
        }

        [HttpGet]
        public IHttpActionResult Update(int id)
        {
            var entity = _coCooperationApplicationService.FindById(id);
            entity.GrabbedById = 1;
            entity.GrabbedByName = "admin";
            entity.GrabbedDate = DateTime.Now;
            _coCooperationApplicationService.Update(entity);
            return Ok("success");
        }

        [HttpGet]
        public IHttpActionResult Delete(int id)
        {
            _coCooperationApplicationService.Delete(id);
            return Ok("success");
        }

        [HttpPost]
        public IHttpActionResult FormValidation(Demo body)
        {
            return Ok(new { message = "success", body });
        }

        [HttpGet]
        public IHttpActionResult AppCost()
        {
            var findAppCostsGroupByAppId = _coSettlementChannelCostService.FindAppCostsGroupByAppId(0, 0, "", "", "CreatedOn").Select(x => x).ToList();
            var findAppCostsGroupByDeveloperId = _coSettlementChannelCostService.FindAppCostsGroupByDeveloperId(0, 0, "", "", "CreatedOn").Select(x => x).ToList();
            var findAppCostsGroupByDeveloperIdAndAppId = _coSettlementChannelCostService.FindAppCostsGroupByDeveloperIdAndAppId(0, 0, "", "", "CreatedOn").Select(x => x).ToList();

            return Ok(new { findAppCostsGroupByAppId, findAppCostsGroupByDeveloperId, findAppCostsGroupByDeveloperIdAndAppId });
        }

        [HttpGet]
        public IHttpActionResult Overview()
        {
            var findPagedListByDeveloperId = _coSettlementDeveloperOverviewService.FindPagedListByDeveloperId(1).Select(x => x);
            var findAll = _coSettlementDeveloperOverviewService.FindAll().Select(x => x);
            var findPagedListByDeveloperName = _coSettlementDeveloperOverviewService.FindPagedListByDeveloperName("").Select(x => x);
            return Ok(new { findPagedListByDeveloperId, findAll, findPagedListByDeveloperName });
        }

        [HttpGet]
        public IHttpActionResult Details()
        {
            var all = _coSettlementDeveloperAppDetailsService.FindAll("Id DESC").Select(x => x).ToList();
            var findByDeveloperId = _coSettlementDeveloperAppDetailsService.FindPagedListByDeveloperId(3).Select(x => x).ToList();
            var findByDeveloperName = _coSettlementDeveloperAppDetailsService.FindPagedListByDeveloperName("技术测试").Select(x => x).ToList();
            return Ok(new { all,findByDeveloperId, findByDeveloperName });
        }
    }

    public class Demo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public int Browser { get; set; }
    }
}
