﻿using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;
using TOOL.EnumUtil;

namespace DxPay.Services
{
    public class StatisticsService : GenericService<jmp_statistics>, IStatisticsService
    {
        private readonly IStatisticsRepository _repository;
        public StatisticsService(IStatisticsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public jmp_statistics FindBytime(string start, string end)
        {

            var where = new List<string>();
            if (!string.IsNullOrEmpty(start))
            {
                where.Add("convert(varchar(10),s_time,120)>='" + start + "' ");
            }
            if (!string.IsNullOrEmpty(end))
            {
              where.Add( "convert(varchar(10),s_time,120)<='" + end + "' ");
            }
            return _repository.FindBytime(string.Join(" AND ", where));
        }

        public IEnumerable<jmp_statistics> FindPagedListByAgent(int userid,  string orderBy, string stime, string etime, string a_name)
        {
            var where = new List<string>();
            if (!string.IsNullOrEmpty(stime))
            {
               where.Add("convert(varchar(10),a.s_time,120)>='" + stime + "' ");
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where.Add(" convert(varchar(10),a.s_time,120)<='" + etime + "' ");
               
            }
            if (a_name!= "所有应用")
            {
                where.Add("b.a_name ='" + a_name + "'");               
            }
           
            
                where.Add("c.relation_type="+ (int)Relationtype.Agent+"");
                where.Add("c.relation_person_id='" + userid + "'");
            
            return _repository.FindListBySql(string.Join(" AND ", where), "");
        }

        public IEnumerable<jmp_statistics> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name)
        {
            var where = new List<string>();
            string bpWhere = "";
            string agentWhere = "";
            if (!string.IsNullOrEmpty(stime))
            {
                where.Add("convert(varchar(10),a.s_time,120)>='" + stime + "' ");
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where.Add(" convert(varchar(10),a.s_time,120)<='" + etime + "' ");

            }
            if (a_name != "所有应用")
            {
                where.Add("b.a_name ='" + a_name + "'");
            }
            bpWhere = "relation_type=" + (int)Relationtype.Bp + " and relation_person_id=" + userid + " ";
            agentWhere = "a.relation_type=" + (int)Relationtype.Agent + " and c.OwnerId=" + userid + "";

            
            return _repository.FindListBySql(string.Join(" AND ", where),bpWhere,agentWhere, "");
        }
    }
}
