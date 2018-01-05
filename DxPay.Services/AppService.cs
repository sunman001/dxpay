using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;

namespace DxPay.Services
{
    public class AppService : GenericService<jmp_app>, IAppService
    {
        private readonly IAppRepository _repository;
        public AppService(IAppRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<jmp_app> FindListByUserId(int classtype, int userid, string orderBy)
        {
            return _repository.FindListByUserId(classtype, userid, orderBy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="calsstype"></param>
        /// <param name="orderBy"></param>
        /// <param name="platformid"></param>
        /// <param name="auditstate"></param>
        /// <param name="sea_name"></param>
        /// <param name="type"></param>
        /// <param name="SelectState"></param>
        /// <param name="searchDesc"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<jmp_app> FindPagedListBySql(int userid, int calsstype, string orderBy, int platformid, int auditstate, string sea_name, int type, int SelectState, int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {

            orderBy = " order by a_id desc";
            string where = "";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        where += "  and a.a_id =' " + sea_name + "'";
                        break;
                    case 2:
                        where += " and a.a_name like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        where += " and b.u_realname like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        where += " and a.a_key =  '" + sea_name + "' ";
                        break;
                }

            }
            if (auditstate > -1)
            {
                where += " and a.a_auditstate='" + auditstate + "' ";
            }
            if (SelectState > -1)
            {
                where += " and a.a_state='" + SelectState + "' ";
            }
            else
            {
                where += "and a.a_state > -1 ";
            }
            if (platformid > 0)
            {
                where += " and a.a_platform_id=" + platformid + "  ";
            }
            if (searchDesc == 1)
            {
                orderBy = " order by a_id  ";
            }
            else
            {
                orderBy = " order by a_id desc ";
            }

            return _repository.FindPagedListBySql(userid, calsstype, orderBy, where, parameters, pageIndex, pageSize);
        }
    }
}
