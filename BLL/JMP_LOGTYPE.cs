using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using JMP.MDL;
namespace JMP.BLL {
	 	//日志类型表
		public partial class jmp_logtype
	{
   		     
		private readonly JMP.DAL.jmp_logtype dal=new JMP.DAL.jmp_logtype();
		public jmp_logtype()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int l_id)
		{
			return dal.Exists(l_id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(JMP.MDL.jmp_logtype model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(JMP.MDL.jmp_logtype model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int l_id)
		{
			
			return dal.Delete(l_id);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string l_idlist )
		{
			return dal.DeleteList(l_idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public JMP.MDL.jmp_logtype GetModel(int l_id)
		{
			
			return dal.GetModel(l_id);
		}

		

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JMP.MDL.jmp_logtype> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JMP.MDL.jmp_logtype> DataTableToList(DataTable dt)
		{
			List<JMP.MDL.jmp_logtype> modelList = new List<JMP.MDL.jmp_logtype>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				JMP.MDL.jmp_logtype model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new JMP.MDL.jmp_logtype();					
													if(dt.Rows[n]["l_id"].ToString()!="")
				{
					model.l_id=int.Parse(dt.Rows[n]["l_id"].ToString());
				}
																																				model.l_name= dt.Rows[n]["l_name"].ToString();
																																model.l_value= dt.Rows[n]["l_value"].ToString();
																						
				
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
#endregion
   
	}
}