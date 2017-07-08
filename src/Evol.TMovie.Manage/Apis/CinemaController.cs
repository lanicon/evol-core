﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Evol.TMovie.Domain.QueryEntries;
using Evol.Domain.Messaging;
using Evol.TMovie.Manage.Models;
using Evol.TMovie.Domain.QueryEntries.Parameters;
using Evol.Common;
using Evol.TMovie.Domain.Commands.Dto;
using Evol.TMovie.Domain.Dto;
using Evol.Web.Exceptions;
using Evol.TMovie.Domain.Commands;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Evol.Util.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Evol.TMovie.Manage.Apis
{
    /// <summary>
    /// 影院CURD
    /// </summary>
    [Route("api/[controller]")]
    public class CinemaController : Controller
    {
        public ICinemaQueryEntry CinemaQueryEntry { get; set; }

        public ICommandBus CommandBus { get; set; }

        public CinemaController(ICinemaQueryEntry cinemaQueryEntry, ICommandBus commandBus)
        {
            CinemaQueryEntry = cinemaQueryEntry;
            CommandBus = commandBus;
        }

        // GET: api/Cinema
        /// <summary>
        /// 查询影院列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CinemaViewModel>> GetSearch([FromQuery]CinemaQueryParameter param = null)
        {
            var list = await CinemaQueryEntry.RetrieveAsync(param);
            var result = list.Map<List<CinemaViewModel>>();
            return result;
        }

        // GET: api/Cinema/Paged
        /// <summary>
        /// 分页查询影院列表
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public async Task<IPaged<CinemaViewModel>> Get([FromQuery]CinemaQueryParameter param = null, int pageIndex = 1, int pageSize = 10)
        {
            var paged = await CinemaQueryEntry.PagedAsync(param, pageIndex, pageSize);
            var result = paged.MapPaged<CinemaViewModel>();
            return result;
        }

        // GET: api/Cinema/5
        /// <summary>
        /// 获取指定影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CinemaViewModel> Get(Guid id)
        {
            var item = await CinemaQueryEntry.FindAsync(id);
            var result = item.Map<CinemaViewModel>();
            return result;
        }

        // POST: api/Cinema
        /// <summary>
        /// 创建影院
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Post(CinemaCreateDto value)
        {
            if (!TryValidateModel(value))
            {
                var keys = ModelState.Keys;
                var errorState = new Dictionary<string, string>();
                foreach (var key in keys)
                {
                    ModelStateEntry modeState = null;
                    if (ModelState.TryGetValue(key, out modeState) && modeState != null && modeState.ValidationState != ModelValidationState.Valid)
                        errorState.Add(key, string.Join(";", modeState.Errors.Select(e => e.ErrorMessage)));
                }

                var exc = new InputException(errorState, string.Join(";", ModelState.Root.Errors.Select(e => e.ErrorMessage)) ?? "输入错误！");
                return JsonUtil.Serialize(exc);
            }

            await CommandBus.SendAsync(new CinemaCreateCommand { Input = value });
            return null;
        }

        // PUT: api/Cinema/5
        /// <summary>
        /// 更新影院
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public async Task Put(int id, CinemaUpdateDto value)
        {
            if (!TryValidateModel(value))
            {
                var errorState = ModelState.Select(e => new KeyValuePair<string, string>(e.Key, e.Value.RawValue.ToString())).ToDictionary(e => e.Key, e => e.Value);
                throw new InputException(errorState, "输入错误");
            }

            await CommandBus.SendAsync(new CinemaUpdateCommand { Input = value });
        }

        // DELETE: api/Cinema/5
        /// <summary>
        /// 删除影院
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await CommandBus.SendAsync(new CinemaDeleteCommand { Input = new ItemDeleteDto { Id = id } });
        }
    }
}