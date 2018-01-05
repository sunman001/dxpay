using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;
using System.Collections;
using JMP.TOOL;
using System.Text;
using TOOL.EnumUtil;

namespace DxPay.Services
{
    public class TrendsService :  ITrendsService
    {
        private readonly ITrendsRepository _repository;
        public TrendsService(ITrendsRepository repository) 
        {
            _repository = repository;
        }
        /// <summary>
        /// 代理商平台分页查询
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderBy"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="a_name"></param>
        /// <returns></returns>
        public IEnumerable<jmp_trends> FindPagedListByAgent(int userid,  string orderBy, string stime, string etime,string a_name)
        {
            var where = new List<string>();
            if (!string.IsNullOrEmpty(stime))
            {
                where.Add( "convert(varchar(10),a.t_time,120)>='" + stime + "' ");
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where.Add("convert(varchar(10),a.t_time,120)<='" + etime + "' ");
            }
            if(a_name!= "所有应用")
            {
                where.Add("b.a_name = '" + a_name + "'");
            }
           
            
                where.Add("c.relation_type="+(int)Relationtype.Agent+"");
                where.Add("c.relation_person_id='" + userid + "'");
            

            return _repository.FindPagedListBySql(string.Join(" AND ", where), "" );
        }
        /// <summary>
        /// 分页查询（商务）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderBy"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="a_name"></param>
        /// <returns></returns>
        public IEnumerable<jmp_trends> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name)
        {
            var where = new List<string>();
            string bpWhere = "";
            string agentWhere = "";
            if (!string.IsNullOrEmpty(stime))
            {
                where.Add("convert(varchar(10),a.t_time,120)>='" + stime + "' ");
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where.Add("convert(varchar(10),a.t_time,120)<='" + etime + "' ");
            }
            if (a_name != "所有应用")
            {
                where.Add("b.a_name = '" + a_name + "'");
            }
           
            bpWhere = "relation_type="+ (int)Relationtype.Bp+" and relation_person_id="+userid+" ";
            agentWhere = "a.relation_type="+(int)Relationtype.Agent+" and c.OwnerId="+userid+"";

            return _repository.FindPagedListByBp(string.Join(" AND ", where),bpWhere,agentWhere, "");
        }
    }
}

