using System.Collections.Generic;

namespace WEB.ViewModel.Scheduler
{
    public class CreateScheduleViewModel
    {
        public CreateScheduleViewModel()
        {
            CreateScheduleUserViewModel = new List<Scheduler.CreateScheduleUserViewModel>();
        }
        /// <summary>
        /// 被选中的值班人员集合
        /// </summary>
        public List<CreateScheduleUserViewModel> CreateScheduleUserViewModel { get; set; }
        public string SelectedUserIds { get; set; }
        /// <summary>
        /// 排班年月
        /// </summary>
        public string ScheduleMonth { get; set; }
    }

    public class CreateScheduleUserViewModel
    {
        public int Id { get; set; }
        public string RealName { get; set; }
    }
}