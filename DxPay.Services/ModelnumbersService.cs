using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;
using TOOL.EnumUtil;

namespace DxPay.Services
{
    public class ModelnumbersService : GenericService<jmp_modelnumber>, IModelnumbersService
    {
        private readonly IModelnumbersRepository _repository;
        public ModelnumbersService(IModelnumbersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public jmp_modelnumber FindBytime(string start, string end)
        {

            var where = new List<string>();
            if (!string.IsNullOrEmpty(start))
            {
                where.Add("  convert(varchar(10),m_time,120)>='" + start + "' ");
            }
            if (!string.IsNullOrEmpty(end))
            {
              where.Add(" convert(varchar(10),m_time,120)<='" + end + "' ");
            }
            return _repository.FindBytime(string.Join(" AND ", where));
        }

        public IEnumerable<jmp_modelnumber> FindPagedList(int userid,  string orderBy, string stime, string etime, string a_name)
        {
            var where = new List<string>();
            if (!string.IsNullOrEmpty(stime))
            {
               where.Add("convert(varchar(10),a.m_time,120)>='" + stime + "' ");
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where.Add(" convert(varchar(10),a.m_time,120)<='" + etime + "' ");
               
            }
            if (a_name!= "所有应用")
            {
                where.Add("b.a_name ='" + a_name + "'");               
            }
             where.Add("c.relation_type="+ (int)Relationtype.Agent+"");
            where.Add("c.relation_person_id='" + userid + "'");
            
            return _repository.FindListBySql(string.Join(" AND ", where), "");
        }

        public IEnumerable<jmp_modelnumber> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name)
        {
            string bpWhere = "";
            string agentWhere = "";
            var where = new List<string>();
            if (!string.IsNullOrEmpty(stime))
            {
                where.Add("convert(varchar(10),a.m_time,120)>='" + stime + "' ");
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where.Add(" convert(varchar(10),a.m_time,120)<='" + etime + "' ");

            }
            if (a_name != "所有应用")
            {
                where.Add("b.a_name ='" + a_name + "'");
            }
            bpWhere = "relation_type=" + (int)Relationtype.Bp + " and relation_person_id=" + userid + " ";
            agentWhere = "a.relation_type=" + (int)Relationtype.Agent + " and c.OwnerId=" + userid + "";
            return _repository.FindListBySql(string.Join(" AND ", where),bpWhere ,agentWhere, "");
        }
    }
}
