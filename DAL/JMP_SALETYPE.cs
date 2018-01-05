using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using JMP.DBA;
namespace JMP.DAL  
{
	 	//销售类型表
		public partial class jmp_saletype
	{
   		     
		public bool Exists(int s_id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from jmp_saletype");
			strSql.Append(" where ");
			                                       strSql.Append(" s_id = @s_id  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@s_id", SqlDbType.Int,4)
			};
			parameters[0].Value = s_id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(JMP.MDL.jmp_saletype model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into jmp_saletype(");			
            strSql.Append("s_name,s_value,s_state");
			strSql.Append(") values (");
            strSql.Append("@s_name,@s_value,@s_state");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@s_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@s_value", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_state", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.s_name;                        
            parameters[1].Value = model.s_value;                        
            parameters[2].Value = model.s_state;                        
			   
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
		public bool Update(JMP.MDL.jmp_saletype model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update jmp_saletype set ");
			                                                
            strSql.Append(" s_name = @s_name , ");                                    
            strSql.Append(" s_value = @s_value , ");                                    
            strSql.Append(" s_state = @s_state  ");            			
			strSql.Append(" where s_id=@s_id ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@s_id", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_name", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@s_value", SqlDbType.Int,4) ,            
                        new SqlParameter("@s_state", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.s_id;                        
            parameters[1].Value = model.s_name;                        
            parameters[2].Value = model.s_value;                        
            parameters[3].Value = model.s_state;                        
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
		public bool Delete(int s_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from jmp_saletype ");
			strSql.Append(" where s_id=@s_id");
						SqlParameter[] parameters = {
					new SqlParameter("@s_id", SqlDbType.Int,4)
			};
			parameters[0].Value = s_id;


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
		public bool DeleteList(string s_idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from jmp_saletype ");
			strSql.Append(" where ID in ("+s_idlist + ")  ");
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
		public JMP.MDL.jmp_saletype GetModel(int s_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select s_id, s_name, s_value, s_state  ");			
			strSql.Append("  from jmp_saletype ");
			strSql.Append(" where s_id=@s_id");
						SqlParameter[] parameters = {
					new SqlParameter("@s_id", SqlDbType.Int,4)
			};
			parameters[0].Value = s_id;

			
			JMP.MDL.jmp_saletype model=new JMP.MDL.jmp_saletype();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["s_id"].ToString()!="")
				{
					model.s_id=int.Parse(ds.Tables[0].Rows[0]["s_id"].ToString());
				}
																																				model.s_name= ds.Tables[0].Rows[0]["s_name"].ToString();
																												if(ds.Tables[0].Rows[0]["s_value"].ToString()!="")
				{
					model.s_value=int.Parse(ds.Tables[0].Rows[0]["s_value"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["s_state"].ToString()!="")
				{
					model.s_state=int.Parse(ds.Tables[0].Rows[0]["s_state"].ToString());
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
			strSql.Append(" FROM jmp_saletype ");
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
			strSql.Append(" FROM jmp_saletype ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

