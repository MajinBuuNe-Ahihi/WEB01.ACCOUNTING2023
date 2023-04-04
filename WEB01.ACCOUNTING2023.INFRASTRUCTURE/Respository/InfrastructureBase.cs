using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.INFRASTRUCTURE.Respository
{
    public class InfrastructureBase<T> : IIfrastructureBase<T>
    {
        #region Field
        IDatabase _dbConnection;
        #endregion
        #region Constructor
        /// <summary>
        ///  constructor
        /// </summary>
        /// <param name="dbConnection">injection của IDatabase</param>
        public InfrastructureBase(IDatabase dbConnection)
        {
            _dbConnection = dbConnection;
        }
        #endregion

        #region Method

        public ResponseResult DeleteDataByID(Guid id)
        {
            var nameGeneric = typeof(T).Name;
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@v_{nameGeneric.Substring(0, nameGeneric.Length - 1)}Id", id);

            if (dynamicParameters != null)
            {
                this._dbConnection.Open();
                var proc = $"Proc_{nameGeneric}_DeleteByID";
                var results =  this._dbConnection.GetConnection().Execute(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                this._dbConnection.Close();
                if (results != 0)
                {
                    return new ResponseResult()
                    {
                        Data = results,
                        ErrorCode = CORE.Enum.ErrorCode.SUCCESS,
                        Message = Resource.Success.ToString(),
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseResult()
                    {
                        Data = null,
                        ErrorCode = CORE.Enum.ErrorCode.NOT_FOUND,
                        Message = Resource.NotFound.ToString(),
                        StatusCode = 404
                    };
                }
            }
            else
            {
                return new ResponseResult()
                {
                    Data = null,
                    ErrorCode = CORE.Enum.ErrorCode.FAIL,
                    Message = Resource.Fail.ToString(),
                    StatusCode = 400
                };
            }
        }

        public ResponseResult GetDataByID(Guid id)
        {
            var nameGeneric = typeof(T).Name;
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@v_{nameGeneric.Substring(0, nameGeneric.Length - 1)}Id", id);
            if (dynamicParameters != null)
            {
                this._dbConnection.Open();
                var proc = $"Proc_{nameGeneric}_GetById";
                var results =    this._dbConnection.GetConnection().QueryFirstOrDefault<T>(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                this._dbConnection.Close();
                if (results != null)
                {
                    return new ResponseResult()
                    {
                        Data = results,
                        ErrorCode = CORE.Enum.ErrorCode.SUCCESS,
                        Message = Resource.Success.ToString(),
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseResult()
                    {
                        Data = null,
                        ErrorCode = CORE.Enum.ErrorCode.NOT_FOUND,
                        Message = Resource.NotFound.ToString(),
                        StatusCode = 404
                    };
                }
            }
            else
            {
                return new ResponseResult()
                {
                    Data = null,
                    ErrorCode = CORE.Enum.ErrorCode.FAIL,
                    Message = Resource.Fail.ToString(),
                    StatusCode = 400
                };
            }
        }

        public ResponseResult GetAllData<G>()
        {
            var nameGeneric = typeof(T).Name;
            this._dbConnection.Open();
            var proc = $"Proc_{nameGeneric}_GetAll";
            var result = this._dbConnection.GetConnection().Query<G>(sql: proc, commandType: CommandType.StoredProcedure);
            this._dbConnection.Close();
            if (result != null)
            {
                return new ResponseResult()
                {
                    Data = result,
                    ErrorCode = CORE.Enum.ErrorCode.SUCCESS,
                    Message = Resource.Success.ToString(),
                    StatusCode = 200
                };
            }
            else
            {
                return new ResponseResult()
                {
                    Data = null,
                    ErrorCode = CORE.Enum.ErrorCode.NOT_FOUND,
                    Message = Resource.NotFound.ToString(),
                    StatusCode = 404
                };
            }
        }
        #endregion
    }
 }
