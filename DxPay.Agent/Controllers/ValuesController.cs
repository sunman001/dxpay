using System.Linq;
using System.Web.Http;
using DxPay.Agent.Models;
using DxPay.Factory;
using DxPay.Services;
using JMP.MDL;

namespace DxPay.Agent.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ICoCooperationApplicationService _coCooperationApplicationService;
        private readonly ICoAgentService _agentService;
        public ValuesController()
        {
            _coCooperationApplicationService = ServiceFactory.CoCooperationApplicationService;
            _agentService = ServiceFactory.CoAgentService;
        }

        [HttpGet]
        [LoginCheckFilter(IsCheck = true,IsRole = true)]
        public IHttpActionResult Get(int id, int pageIndex, [FromUri]int pageSize)
        {
            var list = _coCooperationApplicationService.FindPagedList("Id DESC", "Id>" + id, pageIndex, pageSize);
            //var list = test.Run(id, pageIndex, pageSize);
            var gridModel = new DataSource<CoCooperationApplication>(list)
            {
                Data = list.Select(x => x)
            };

            return Ok(gridModel);
        }

        [HttpGet]
        public IHttpActionResult Unlock()
        {
            var success = _agentService.UnLock(1);
            return Ok(success);
        }

        [HttpGet]
        public IHttpActionResult Lock()
        {
            var success = _agentService.Lock(1);
            return Ok(success);
        }

    }
}
