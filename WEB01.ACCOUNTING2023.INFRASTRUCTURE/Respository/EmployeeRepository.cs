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
    public class EmployeeRepository:InfrastructureBase<Employee>,IEmployeeRepository
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
        public EmployeeRepository(IDatabase dbConnection):base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
        #endregion

        #region Method

        /// <summary>
        ///  xóa nhiều
        /// </summary>
        /// <param name="ids"> string chứa nhiều id</param>
        /// <returns></returns>
        public ResponseResult DeleteEmployees(string ids)
        {
            this._dbConnection.GetConnection().Open();
            using (var transaction = this._dbConnection.GetConnection().BeginTransaction())
            {
                try
                {
                    // khởi tạo tham số proc
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@v_EmployeeIds", ids);
                    if (dynamicParameters != null)
                    {
                        var numberRecordDelete = ids.Split(",").Length;
                        // khởi tạo kết nối, lấy dữ liệu
                        var proc = "Proc_Employee_Delete";
                        var results = this._dbConnection.GetConnection().Execute(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure,transaction:transaction);
                        // trả về dữ liệu
                        if (results == numberRecordDelete)
                        {
                            transaction.Commit();
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
                            transaction.Rollback();
                            return new ResponseResult()
                            {
                                Data = null,
                                ErrorCode = CORE.Enum.ErrorCode.FAIL,
                                Message = Resource.Fail.ToString(),
                                StatusCode = 400
                            };
                        }
                    }
                    else
                    { 
                        transaction.Rollback();
                        return new ResponseResult()
                        {
                            Data = null,
                            ErrorCode = CORE.Enum.ErrorCode.FAIL,
                            Message = Resource.Fail.ToString(),
                            StatusCode = 400
                        };
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }finally
                {
                    this._dbConnection.GetConnection().Close();
                }
            }
           
        }

        /// <summary>
        ///  lấy dữ liệu số lượng bản ghi dựa vào mã code
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="code">mã code nhân viên</param>
        /// <returns></returns>
        public ResponseResult GetDataByCode(string code)
        {
            try
            {
                // khởi tạo tham số proc
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@v_EmployeeCode", code);
                string proc = "Proc_Employee_GetByCode";

                if (dynamicParameters != null)
                {
                    // khởi tạo kết nối, lấy dữ liệu
                    this._dbConnection.Open();
                    var result = (this._dbConnection.GetConnection()).QueryFirstOrDefault<EmployeeDTO>(sql: proc, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                    this._dbConnection.Close();
                    // trả về dữ liệu
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
            catch (Exception ex)
            {
                this._dbConnection.Close();
                throw ex;
            }
        }

        /// <summary>
        ///  lấy thông tin nhân viên thông qua danh sách id
        /// </summary>
        /// <param name="ids">id những nhân viên cần lấy thông tin</param>
        /// <returns></returns>
        public ResponseResult GetEmployeeByIDs(string ids)
        {
            try
            {
                // khởi tạo tham số proc
              DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@v_EmployeeIds", ids);

            if (dynamicParameters != null && ids != null)
            {
                    // khởi tạo kết nối, lấy dữ liệu
                this._dbConnection.Open();
                var proc = "Proc_Employee_GetByIds";
                var results = (this._dbConnection.GetConnection().Query<EmployeeDTO>(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));
                 results = results.ToList<EmployeeDTO>();
                this._dbConnection.Close();
                // trả về dữ liệu
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
            }catch (Exception ex) {
                this._dbConnection.Close();
                throw ex;
            }
        }

        /// <summary>
        ///  lấy danh sach nhân viên dựa trên keyword
        ///  create by: HV Mạnh (9/4/2023)
        /// </summary>
        /// <param name="keyWord">từ khóa</param>
        /// <returns></returns>
        public ResponseResult GetEmployeeByKeyWord<T>(string keyWord="")
        {
            try
            {
                // khởi tạo tham số proc
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@v_KeyWord", keyWord == null ?"":keyWord);

                if (dynamicParameters != null)
                {
                    // khởi tạo kết nối, lấy dữ liệu
                    this._dbConnection.Open();
                    var proc = "Proc_Employee_GetByKeyWord";
                    var results = this._dbConnection.GetConnection().Query<T>(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure).ToList<T>();
                    this._dbConnection.Close();
                    // trả về dữ liệu
                    if (results !=  null)
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
            catch (Exception ex)
            {
                this._dbConnection.Close();
                throw ex;
            }

        }

        /// <summary>
        ///  lấy danh sách đối tượng theo từ khóa và số bản ghi
        ///  author: HV Mạnh
        ///  createdate: 13/3/2023
        /// </summary>
        /// <param name="pageSize">kích thước 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="key">từ khóa</param>
        /// <returns></returns>
        public ResponseResult GetFilterData(int pageSize, int pageNumber, string? key)
        {
            try
            {
                // khởi tạo tham số proc
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@v_PageNumber", (pageNumber - 1) * pageSize);
                dynamicParameters.Add("@v_PageSize", pageSize);
                dynamicParameters.Add("@v_KeyWord", key);
        
                if (dynamicParameters != null)
                {
                    // thực hiện kết nối, lấy dữ liệu
                    this._dbConnection.Open();
                    var proc = "Proc_Employee_Filter";
                    var multipleResults = (this._dbConnection.GetConnection()).QueryMultiple(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    var employees = multipleResults.Read<EmployeeDTO>();
                    var totalCount = multipleResults.Read<long>().Single();
                    this._dbConnection.Close();
                    // trả về dữ liệu
                    return new ResponseResult()
                    {
                        Data = new
                        {
                            Values = employees,
                            Count = totalCount
                        },
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
                        ErrorCode = CORE.Enum.ErrorCode.FAIL,
                        Message = Resource.Success.ToString(),
                        StatusCode = 400
                    };
                }
            }
            catch(Exception ex)
            {
                this._dbConnection.Close();
                throw ex;
            }
        }

        /// <summary>
        ///  lấy danh  mã code mới
        ///  author: HV Mạnh
        ///  createdate: 13/3/2023
        /// </summary>
        /// <returns>mã code</returns>
        public ResponseResult GetNewCode()
        {
            try
            {
                // khởi tạo kết nối lấy nối lấy dữ liệu
                this._dbConnection.Open();
                var proc = "Proc_Employee_GetMaxCode";
                var result = this._dbConnection.GetConnection().QueryFirstOrDefault<Nullable<int>>(proc, commandType: System.Data.CommandType.StoredProcedure);
                this._dbConnection.Close();
                // trả về dữ liệu
                if (result != null)
                {
                    return new ResponseResult()
                    {
                        Data = new NewCodeResult("NV") { Code = ((int)result + 1).ToString() },
                        ErrorCode = CORE.Enum.ErrorCode.SUCCESS,
                        Message = Resource.Success.ToString(),
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseResult()
                    {
                        Data = new NewCodeResult("NV") { Code = "1" },
                        ErrorCode = CORE.Enum.ErrorCode.SUCCESS,
                        Message = Resource.Success.ToString(),
                        StatusCode = 200
                    };
                }
            }
            catch (Exception ex)
            {
                this._dbConnection.Close();
                throw ex;
            }
        }

        /// <summary>
        ///  thêm dữ liệu
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="data">Đố tượng nhân viên</param>
        /// <returns>số dòng tác động</returns>
        public ResponseResult InsertData(Employee data)
        {
           try
            {
                // khởi tạo tham số proc
                var proc = "Proc_Employee_Insert";
                var listVaribleProc = this._dbConnection.GetVaribleProc(proc);
                DynamicParameters dynamicParameters = new DynamicParameters();

                // duyệt lấy các thông tin từ property data của proc -> lấy dữ liệu từ entity
                listVaribleProc.ForEach(item =>
                {
                    var nameProperty = item.Substring(3);
                    var info = data.GetType().GetProperty(nameProperty);
                    var value = info.GetValue(data, null);
                    dynamicParameters.Add(item.ToString(), value);

                });

                if (dynamicParameters != null)
                {
                    // khởi tạo kết nối, lấy dữ liệu
                    this._dbConnection.Open();
                    var result = this._dbConnection.GetConnection().Execute(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    this._dbConnection.Close();
                    // trả về dữ liệu
                    if (result != 0)
                    {
                        return new ResponseResult()
                        {
                            Data = result,
                            ErrorCode = CORE.Enum.ErrorCode.SUCCESS,
                            Message = Resource.Success.ToString(),
                            StatusCode = 201
                        };
                    }
                    else
                    {
                        return new ResponseResult() { Data = null, ErrorCode = CORE.Enum.ErrorCode.FAIL, Message = Resource.Fail.ToString(), StatusCode = 400 };
                    }
                }
                else
                {
                    return new ResponseResult() { Data = null, ErrorCode = CORE.Enum.ErrorCode.FAIL, Message = Resource.Fail.ToString(), StatusCode = 400 };
                }
            }
            catch (Exception ex)
            {
                this._dbConnection.Close();
                throw ex;
            }
        }


        /// <summary>
        ///  cập nhật data theo id
        ///  createby: HVManh (13/3/2023)
        /// </summary>
        /// <param name="data">đối tượng nhân viên</param>
        /// <param name="id">mã của nhân viên</param>
        /// <returns></returns>
        public ResponseResult UpdateData(Employee data, Guid id)
        {
            try
            {
                // khởi tạo tham số proc
                var proc = "Proc_Employee_UpdateById";
                var listVaribleProc = this._dbConnection.GetVaribleProc(proc);
                DynamicParameters dynamicParameters = new DynamicParameters();
                // đọc dữ liệu từ entity thông qua các property của proc
                listVaribleProc.ForEach(item =>
                {
                    var nameProperty = item.Substring(3);
                    var info = data.GetType().GetProperty(nameProperty);
                    var value = info.GetValue(data, null);
                    dynamicParameters.Add(item.ToString(), value);

                });

                if (dynamicParameters != null)
                {
                    // khởi tạo kết nối
                    this._dbConnection.Open();
                    var result = this._dbConnection.GetConnection().Execute(proc, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    this._dbConnection.Close();
                    // trả về dữ liệu
                    if (result != 0)
                    {
                        return new ResponseResult()
                        {
                            Data = result,
                            ErrorCode = CORE.Enum.ErrorCode.SUCCESS,
                            Message = Resource.Success.ToString(),
                            StatusCode = 201
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
            catch (Exception ex)
            {
                this._dbConnection.Close();
                throw ex;
            }
        }
        #endregion
    }
}
