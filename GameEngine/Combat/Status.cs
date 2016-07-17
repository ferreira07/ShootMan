using GameEngine.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Combat
{
    public class Status
    {
        public MapObject AffectedObject { get; set; }

        public Status(EStatusType statusType, TimeSpan remainTime, TimeSpan timeCicle)
        {
            StatusType = statusType;
            RemainTime = remainTime;
            TimeCicle = timeCicle;
            RemainTimeCicle = timeCicle;
            TimeCount = true;
        }

        public Status(EStatusType statusType)
        {
            StatusType = statusType;
            TimeCount = false;
        }

        public EStatusType StatusType { get; set; }

        public bool TimeCount { get; private set; }
        public TimeSpan RemainTime { get; set; }

        public TimeSpan TimeCicle { get; set; }
        public TimeSpan RemainTimeCicle { get; set; }
        public Attack TimeCicleAttack { get; set; }

        /// <summary>
        /// Passar o tempo para o status
        /// </summary>
        /// <param name="time">Tempo passado</param>
        /// <returns>True se o tempo do efeito já acabou, false se ainda continua.</returns>
        public bool PassTime(TimeSpan time)
        {
            bool statusEnd = true;
            if (RemainTime > TimeSpan.Zero)
            {
                statusEnd = false;

                RemainTimeCicle -= time;
                if (RemainTimeCicle <= TimeSpan.Zero)
                {
                    RemainTimeCicle = TimeCicle;
                    OnTimeCicle();
                }
                RemainTime -= time;
                if (RemainTime <= TimeSpan.Zero)
                {
                    OnDeactivate();
                    statusEnd = true;
                }
            }
            return statusEnd;
        }

        public void OnActivate()
        {

        }
        public void OnDeactivate()
        {

        }
        public void OnTimeCicle()
        {
            if (TimeCicleAttack != null)
            {
                AffectedObject.Map.DoAttack(TimeCicleAttack, AffectedObject);
            }
        }
    }
}
