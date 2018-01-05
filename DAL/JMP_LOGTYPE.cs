using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using JMP.DBA;
namespace JMP.DAL  
{
	 	//日志类型表
		public partial class jmp_logtype
	{
   		     
		public bool Exists(int l_id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from jmp_logtype");
			strSql.Append(" where ");
			                                       strSql.Append(" l_id = @l_id  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@l_id", SqlDbType.Int,4)
			};
			parameters[0].Value = l_id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(JMP.MDL.jmp_logtype model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into jmp_logtype(");			
            strSql.Append("l_name,l_value");
			strSql.Append(") values (");
            strSql.Append("@l_name,@l_value");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@l_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_value", SqlDbType.NVarChar,-1)             
              
            };
			            
            parameters[0].Value = model.l_name;                        
            parameters[1].Value = model.l_value;                        
			   
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
		public bool Update(JMP.MDL.jmp_logtype model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update jmp_logtype set ");
			                                                
            strSql.Append(" l_name = @l_name , ");                                    
            strSql.Append(" l_value = @l_value  ");            			
			strSql.Append(" where l_id=@l_id ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@l_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@l_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@l_value", SqlDbType.NVarChar,-1)             
              
            };
						            
            parameters[0].Value = model.l_id;                        
            parameters[1].Value = model.l_name;                        
            parameters[2].Value = model.l_value;                        
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
		public bool Delete(int l_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from jmp_logtype ");
			strSql.Append(" where l_id=@l_id");
						SqlParameter[] parameters = {
					new SqlParameter("@l_id", SqlDbType.Int,4)
			};
			parameters[0].Value = l_id;


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
		public bool DeleteList(string l_idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from jmp_logtype ");
			strSql.Append(" where ID in ("+l_idlist + ")  ");
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
		public JMP.MDL.jmp_logtype GetModel(int l_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select l_id, l_name, l_value  ");			
			strSql.Append("  from jmp_logtype ");
			strSql.Append(" where l_id=@l_id");
						SqlParameter[] parameters = {
					new SqlParameter("@l_id", SqlDbType.Int,4)
			};
			parameters[0].Value = l_id;

			
			JMP.MDL.jmp_logtype model=new JMP.MDL.jmp_logtype();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["l_id"].ToString()!="")
				{
					model.l_id=int.Parse(ds.Tables[0].Rows[0]["l_id"].ToString());
				}
																																				model.l_name= ds.Tables[0].Rows[0]["l_name"].ToString();
																																model.l_value= ds.Tables[0].Rows[0]["l_value"].ToString();
																										
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
			strSql.Append(" FROM jmp_logtype ");
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
			strSql.Append(" FROM jmp_logtype ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

