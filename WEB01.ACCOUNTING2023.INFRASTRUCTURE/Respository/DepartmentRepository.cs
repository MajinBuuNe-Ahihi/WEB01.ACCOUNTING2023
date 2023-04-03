using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Enum;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Resource;

namespace WEB01.ACCOUNTING2023.INFRASTRUCTURE.Respository
{
    public class DepartmentRepository : InfrastructureBase<Departments>, IDepartmentRepository
    {
        #region Field
        IDatabase _dbConnection;
        #endregion

        #region Constructor

        /// <summary>
        ///   constructor
        ///   createby: HV Mạnh (16/3/2023)
        /// </summary>
        /// <param name="dbConnection">injection của database</param>
        public DepartmentRepository(IDatabase dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
        #endregion

        #region Method

        public ResponseResult  GetAllData()
        {
            this._dbConnection.Open();
            string proc = "Proc_Departments_GetAll";
            var result = this._dbConnection.GetConnection().Query<DepartmentsDTO>(sql: proc, commandType: CommandType.StoredProcedure);
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

        public  ResponseResult GetDataByFilterValue(string value)
        {
            this._dbConnection.Open();
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@v_Value", value);
            string proc = "Proc_Departments_Filter";
            if (dynamicParameters != null)
            {
                var results = (this._dbConnection.GetConnection()).Query<Departments>(sql: proc, param: dynamicParameters, commandType: CommandType.StoredProcedure);
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
        #endregion
    }
}
