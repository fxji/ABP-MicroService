using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AAA.A3Management.Dto;
using AAA.Models;
using Microsoft.AspNetCore.Authorization;
using AAA.Permissions;
using Email.SendEmail;


namespace AAA.A3Management
{
    [Authorize(AAAPermissions.A3.Default)]
    public class A3AppService : ApplicationService, IA3AppService
    {
        private const string FormName = "A3";
        private IRepository<A3, Guid> _repository;
        private readonly ISendEmailAppService _sendEmailAppService;


        public A3AppService(
            IRepository<A3, Guid> repository, ISendEmailAppService sendEmailAppService
            )
        {
            _repository = repository; _sendEmailAppService = sendEmailAppService;
        }
        #region 增删改查基础方法

        public async Task<A3Dto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<A3, A3Dto>(data);
            return dto;
        }

        public async Task<PagedResultDto<A3Dto>> GetAll(GetA3InputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<A3>, List<A3Dto>>(items);
            return new PagedResultDto<A3Dto>(totalCount, dto);
        }

        public async Task<A3Dto> DataPost(CreateOrUpdateA3Dto input)
        {
            A3 result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateA3Dto, A3>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }



            return ObjectMapper.Map<A3, A3Dto>(result);
        }

        public async Task Share(ShareDto input)
        {
            await _sendEmailAppService.Send(
                 new EmailSendingArgs
                 {
                     EmailAddress = input.EmailAddress,
                     Subject = "Message u need to known",
                     Body = $"A3Report {input.A3.Id}:{input.A3.Name} was created, pls check and confirm"
                 }
            );
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }

        }


        #endregion

    }
}