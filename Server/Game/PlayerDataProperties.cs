using System;
using System.Collections.Generic;
using YSFreedom.Common.Protocol;

namespace YSFreedom.Server.Game
{
    public class PlayerDataProperties : Dictionary<uint, PropValue>
    {
        public long XP {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_EXP); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_EXP, value); }
        }

        public long OriginalResin
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_RESIN); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_RESIN, value); }
        }

        public long IsMpModeAvailable
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_MP_MODE_AVAILABLE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_MP_MODE_AVAILABLE, value); }
        }

        public long PlayerLevel
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_LEVEL); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_LEVEL, value); }
        }

        public long CurrentTempStamina
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_CUR_TEMPORARY_STAMINA); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_CUR_TEMPORARY_STAMINA, value); }
        }

        public long Primogems
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_HCOIN); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_HCOIN, value); }
        }

        public long SCoin
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_SCOIN); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_SCOIN, value); }
        }

        public long MPSettingType
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_MP_SETTING_TYPE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_MP_SETTING_TYPE, value); }
        }

        public long IsOnlyMpWithPsPlayer
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_ONLY_MP_WITH_PS_PLAYER); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_ONLY_MP_WITH_PS_PLAYER, value); }
        }

        public long PlayerForgePoint
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_FORGE_POINT); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_FORGE_POINT, value); }
        }

        public long HasFirstShare
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_HAS_FIRST_SHARE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_HAS_FIRST_SHARE, value); }
        }

        public long PlayerWaitSubHCoin
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WAIT_SUB_HCOIN); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WAIT_SUB_HCOIN, value); }
        }

        public long PlayerWaitSubSCoin
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WAIT_SUB_SCOIN); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WAIT_SUB_SCOIN, value); }
        }

        public long PlayerMCoin
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_MCOIN); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_MCOIN, value); }
        }

        public long PlayerWaitSubMCoin
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WAIT_SUB_MCOIN); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WAIT_SUB_MCOIN, value); }
        }

        public long PlayerLegendaryKey
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_LEGENDARY_KEY); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_LEGENDARY_KEY, value); }
        }

        public long CurClimateMeter
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_METER); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_METER, value); }
        }

        public long CurClimateType
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_TYPE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_TYPE, value); }
        }

        public long CurClimateAreaID
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_AREA_ID); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_AREA_ID, value); }
        }

        public long CurClimateAreaClimateType
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_AREA_CLIMATE_TYPE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_CUR_CLIMATE_AREA_CLIMATE_TYPE, value); }
        }

        public long PlayerWorldLevelLimit
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL_LIMIT); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL_LIMIT, value); }
        }
        public long PlayerWorldLevelAdjustCD
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL_ADJUST_CD); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL_ADJUST_CD, value); }
        }

        public long PlayerLegendaryTaskNum
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_LEGENDARY_DAILY_TASK_NUM); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_LEGENDARY_DAILY_TASK_NUM, value); }
        }

        public long IsWeatherLocked
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_WEATHER_LOCKED); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_WEATHER_LOCKED, value); }
        }

        public long IsSpringAutoUse
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_SPRING_AUTO_USE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_SPRING_AUTO_USE, value); }
        }

        public long SpringAutoUsePercent
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_SPRING_AUTO_USE_PERCENT); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_SPRING_AUTO_USE_PERCENT, value); }
        }

        public long IsFlyable
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_FLYABLE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_FLYABLE, value); }
        }

        public long IsGameTimeLocked
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_GAME_TIME_LOCKED); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_GAME_TIME_LOCKED, value); }
        }

        public long IsTransferrable
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_IS_TRANSFERABLE); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_IS_TRANSFERABLE, value); }
        }

        public long MaxStamina
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_MAX_STAMINA); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_MAX_STAMINA, value); }
        }

        public long CurPersistStamina
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_CUR_PERSIST_STAMINA); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_CUR_PERSIST_STAMINA, value); }
        }

        public long WorldLevel
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL, value); }
        }

        public long WorldLevelLimit
        {
            get { return GetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL_LIMIT); }
            set { SetIVal((uint)EPlayerDataProperties.PROP_PLAYER_WORLD_LEVEL_LIMIT, value); }
        }
        public void SetIVal(uint key, long val)
        {
            this[key] = new PropValue { Type = key, Val = val, Ival = val };
        }

        public long GetIVal(uint key)
        {
            PropValue propValue;
            if (!this.TryGetValue(key, out propValue))
                return 0;

            return propValue.Ival;
        }
    }
}