using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CountingTestContent;

namespace SchoolManagement.Services.Implementation
{
    /// <summary>
    /// Provides data access for counting test content.
    /// </summary>
    public class CountingTestContentImpl : ICountingTestContentService
    {
        private readonly SchoolManagementDb _db;

        public CountingTestContentImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public List<CountingTestContentRespounceVM> GetByTestId(int testId)
        {
            return _db.CountingTestContents
                .Where(c => c.TestId == testId)
                .Include(c => c.Test)
                .Select(c => new CountingTestContentRespounceVM
                {
                    Id = c.Id,
                    Label = c.Label,
                    Shape = c.Shape,
                    Count = c.Count,
                    TestId = c.TestId,
                    TestTitle = c.Test != null ? c.Test.name : null
                })
                .ToList();
        }

        /// <inheritdoc/>
        public CountingTestContentRespounceVM GetById(int id)
        {
            var entity = _db.CountingTestContents
                .Include(c => c.Test)
                .FirstOrDefault(c => c.Id == id);

            if (entity == null)
                return null;

            return new CountingTestContentRespounceVM
            {
                Id = entity.Id,
                Label = entity.Label,
                Shape = entity.Shape,
                Count = entity.Count,
                TestId = entity.TestId,
                TestTitle = entity.Test != null ? entity.Test.name : null
            };
        }

        public CountingTestContentRespounceVM Create(CountingTestContentRequestVM model)
        {
           var newContent = new Model.MCountingTestContent
            {
                Label = model.Label,
                Shape = model.Shape,
                Count = model.Count,
                TestId = model.TestId
            };
            _db.CountingTestContents.Add(newContent);
            _db.SaveChanges();
            return new CountingTestContentRespounceVM
            {
                Id = newContent.Id,
                Label = newContent.Label,
                Shape = newContent.Shape,
                Count = newContent.Count,
                TestId = newContent.TestId
            };
        }
    }
    }