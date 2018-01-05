using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using JMP.MDL;
namespace JMP.BLL {
	 	//应用平台表
		public partial class jmp_platform
	{
   		     
		private readonly JMP.DAL.jmp_platform dal=new JMP.DAL.jmp_platform();
		public jmp_platform()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int p_id)
		{
			return dal.Exists(p_id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(JMP.MDL.jmp_platform model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(JMP.MDL.jmp_platform model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int p_id)
		{
			
			return dal.Delete(p_id);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string p_idlist )
		{
			return dal.DeleteList(p_idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public JMP.MDL.jmp_platform GetModel(int p_id)
		{
			
			return dal.GetModel(p_id);
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
		public List<JMP.MDL.jmp_platform> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<JMP.MDL.jmp_platform> DataTableToList(DataTable dt)
		{
			List<JMP.MDL.jmp_platform> modelList = new List<JMP.MDL.jmp_platform>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				JMP.MDL.jmp_platform model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new JMP.MDL.jmp_platform();					
													if(dt.Rows[n]["p_id"].ToString()!="")
				{
					model.p_id=int.Parse(dt.Rows[n]["p_id"].ToString());
				}
																																				model.p_name= dt.Rows[n]["p_name"].ToString();
																																model.p_value= dt.Rows[n]["p_value"].ToString();
																												if(dt.Rows[n]["p_state"].ToString()!="")
				{
					model.p_state=int.Parse(dt.Rows[n]["p_state"].ToString());
				}
																										
				
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