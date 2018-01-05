using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using JMP.DBA;
namespace JMP.DAL  
{
	 	//应用平台表
		public partial class jmp_platform
	{
   		     
		public bool Exists(int p_id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from jmp_platform");
			strSql.Append(" where ");
			                                       strSql.Append(" p_id = @p_id  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@p_id", SqlDbType.Int,4)
			};
			parameters[0].Value = p_id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(JMP.MDL.jmp_platform model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into jmp_platform(");			
            strSql.Append("p_name,p_value,p_state");
			strSql.Append(") values (");
            strSql.Append("@p_name,@p_value,@p_state");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@p_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@p_value", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@p_state", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.p_name;                        
            parameters[1].Value = model.p_value;                        
            parameters[2].Value = model.p_state;                        
			   
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                    
            	return Convert.ToInt32(obj);
                                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(JMP.MDL.jmp_platform model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update jmp_platform set ");
			                                                
            strSql.Append(" p_name = @p_name , ");                                    
            strSql.Append(" p_value = @p_value , ");                                    
            strSql.Append(" p_state = @p_state  ");            			
			strSql.Append(" where p_id=@p_id ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@p_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@p_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@p_value", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@p_state", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.p_id;                        
            parameters[1].Value = model.p_name;                        
            parameters[2].Value = model.p_value;                        
            parameters[3].Value = model.p_state;                        
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int p_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from jmp_platform ");
			strSql.Append(" where p_id=@p_id");
						SqlParameter[] parameters = {
					new SqlParameter("@p_id", SqlDbType.Int,4)
			};
			parameters[0].Value = p_id;


			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string p_idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from jmp_platform ");
			strSql.Append(" where ID in ("+p_idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public JMP.MDL.jmp_platform GetModel(int p_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select p_id, p_name, p_value, p_state  ");			
			strSql.Append("  from jmp_platform ");
			strSql.Append(" where p_id=@p_id");
						SqlParameter[] parameters = {
					new SqlParameter("@p_id", SqlDbType.Int,4)
			};
			parameters[0].Value = p_id;

			
			JMP.MDL.jmp_platform model=new JMP.MDL.jmp_platform();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["p_id"].ToString()!="")
				{
					model.p_id=int.Parse(ds.Tables[0].Rows[0]["p_id"].ToString());
				}
																																				model.p_name= ds.Tables[0].Rows[0]["p_name"].ToString();
																																model.p_value= ds.Tables[0].Rows[0]["p_value"].ToString();
																												if(ds.Tables[0].Rows[0]["p_state"].ToString()!="")
				{
					model.p_state=int.Parse(ds.Tables[0].Rows[0]["p_state"].ToString());
				}
																														
				return model;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM jmp_platform ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM jmp_platform ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

