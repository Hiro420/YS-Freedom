﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSFreedom.Common.Protocol
{
    public enum EMsgType
    {
        // Commented types doesn't have definitions aviable, u_LuaCodeNotify(1110) name is guessed (AntiCheat Stuff).
        Invalid = 0,

        KeepAliveNotify = 1,
        GmTalkReq = 2,
        GmTalkRsp = 3,
        ShowMessageNotify = 4,
        PingReq = 5,
        PingRsp = 6,
        GetOnlinePlayerListReq = 8,
        GetOnlinePlayerListRsp = 9,
        ServerTimeNotify = 10,
        ServerLogNotify = 11,
        ClientReconnectNotify = 12,
        //ClientFpsStatusNotify = 13,
        RobotPushPlayerDataNotify = 14,

        GetPlayerTokenReq = 101,
        GetPlayerTokenRsp = 102,
        PlayerLoginReq = 103,
        PlayerLoginRsp = 104,
        PlayerLogoutReq = 105,
        PlayerLogoutRsp = 106,
        PlayerLogoutNotify = 107,
        PlayerDataNotify = 108,
        ChangeGameTimeReq = 109,
        ChangeGameTimeRsp = 110,
        PlayerGameTimeNotify = 111,
        PlayerPropNotify = 112,
        ClientTriggerEventNotify = 113,
        SetPlayerPropReq = 114,
        SetPlayerPropRsp = 115,
        SetPlayerBornDataReq = 116,
        SetPlayerBornDataRsp = 117,
        DoSetPlayerBornDataNotify = 118,
        PlayerPropChangeNotify = 119,
        SetPlayerNameReq = 120,
        SetPlayerNameRsp = 121,
        SetOpenStateReq = 122,
        SetOpenStateRsp = 123,
        OpenStateUpdateNotify = 124,
        OpenStateChangeNotify = 125,
        PlayerCookReq = 126,
        PlayerCookRsp = 127,
        PlayerRandomCookReq = 128,
        PlayerRandomCookRsp = 129,
        CookDataNotify = 130,
        CookRecipeDataNotify = 131,
        CookGradeDataNotify = 132,
        PlayerCompoundMaterialReq = 133,
        PlayerCompoundMaterialRsp = 134,
        TakeCompoundOutputReq = 135,
        TakeCompoundOutputRsp = 136,
        CompoundDataNotify = 137,
        GetCompoundDataReq = 138,
        GetCompoundDataRsp = 139,
        PlayerTimeNotify = 140,
        PlayerSetPauseReq = 141,
        PlayerSetPauseRsp = 142,
        PlayerSetLanguageReq = 143,
        PlayerSetLanguageRsp = 144,
        DataResVersionNotify = 145,
        DailyTaskDataNotify = 146,
        DailyTaskProgressNotify = 147,
        DailyTaskScoreRewardNotify = 148,
        //HostDailyTaskNotify = 149, // WorldOwnerDailyTaskNotify
        AddRandTaskInfoNotify = 150,
        RemoveRandTaskInfoNotify = 151,
        TakePlayerLevelRewardReq = 152,
        TakePlayerLevelRewardRsp = 153,
        PlayerLevelRewardUpdateNotify = 154,
        GivingRecordNotify = 155,
        GivingRecordChangeNotify = 156,
        ItemGivingReq = 157,
        ItemGivingRsp = 158,
        PlayerCookArgsReq = 159,
        PlayerCookArgsRsp = 160,

        PlayerEnterSceneNotify = 201,
        LeaveSceneReq = 202,
        LeaveSceneRsp = 203,
        SceneInitFinishReq = 204,
        SceneInitFinishRsp = 205,
        SceneEntityAppearNotify = 206,
        SceneEntityDisappearNotify = 207,
        SceneEntityMoveReq = 208,
        SceneEntityMoveRsp = 209,
        SceneAvatarStaminaStepReq = 210,
        SceneAvatarStaminaStepRsp = 211,
        SceneEntityMoveNotify = 212,
        ScenePlayerLocationNotify = 213,
        GetScenePointReq = 214,
        GetScenePointRsp = 215,
        EnterTransPointRegionNotify = 216,
        ExitTransPointRegionNotify = 217,
        ScenePointUnlockNotify = 218,
        SceneTransToPointReq = 219,
        SceneTransToPointRsp = 220,
        EntityJumpNotify = 221,
        GetSceneAreaReq = 222,
        GetSceneAreaRsp = 223,
        SceneAreaUnlockNotify = 224,
        SceneEntityDrownReq = 225,
        SceneEntityDrownRsp = 226,
        SceneCreateEntityReq = 227,
        SceneCreateEntityRsp = 228,
        SceneDestroyEntityReq = 229,
        SceneDestroyEntityRsp = 230,
        SceneForceUnlockNotify = 231,
        SceneForceLockNotify = 232,
        EnterWorldAreaReq = 233,
        EnterWorldAreaRsp = 234,
        EntityForceSyncReq = 235,
        EntityForceSyncRsp = 236,
        //SceneAreaExploreNotify = 237,
        //SceneGetAreaExplorePercentReq = 238,
        //SceneGetAreaExplorePercentRsp = 239,
        ClientTransmitReq = 240,
        ClientTransmitRsp = 241,
        EnterSceneWeatherAreaNotify = 242,
        ExitSceneWeatherAreaNotify = 243,
        SceneAreaWeatherNotify = 244,
        ScenePlayerInfoNotify = 245,
        WorldPlayerLocationNotify = 246,
        BeginCameraSceneLookNotify = 247,
        EndCameraSceneLookNotify = 248,
        MarkEntityInMinMapNotify = 249,
        UnmarkEntityInMinMapNotify = 250,
        DropSubfieldReq = 251,
        DropSubfieldRsp = 252,
        ExecuteGroupTriggerReq = 253,
        ExecuteGroupTriggerRsp = 254,
        LevelupCityReq = 255,
        LevelupCityRsp = 256,
        SceneRouteChangeNotify = 257,
        PlatformStartRouteNotify = 258,
        PlatformStopRouteNotify = 259,
        PlatformChangeRouteNotify = 260,
        ScenePlayerSoundNotify = 261,
        PersonalSceneJumpReq = 262,
        PersonalSceneJumpRsp = 263,
        SealBattleBeginNotify = 264,
        SealBattleEndNotify = 265,
        SealBattleProgressNotify = 266,
        ClientPauseNotify = 267,
        PlayerEnterSceneInfoNotify = 268,
        JoinPlayerSceneReq = 269,
        JoinPlayerSceneRsp = 270,
        SceneKickPlayerReq = 271,
        SceneKickPlayerRsp = 272,
        SceneKickPlayerNotify = 273,
        HitClientTrivialNotify = 274,
        BackMyWorldReq = 275,
        BackMyWorldRsp = 276,
        SeeMonsterReq = 277,
        SeeMonsterRsp = 278,
        AddSeenMonsterNotify = 279,
        AllSeenMonsterNotify = 280,
        SceneTimeNotify = 281,
        EnterSceneReadyReq = 282,
        EnterSceneReadyRsp = 283,
        EnterScenePeerNotify = 284,
        EnterSceneDoneReq = 285,
        EnterSceneDoneRsp = 286,
        WorldPlayerDieNotify = 287,
        WorldPlayerReviveReq = 288,
        WorldPlayerReviveRsp = 289,
        JoinPlayerFailNotify = 290,
        SetSceneWeatherAreaReq = 291,
        SetSceneWeatherAreaRsp = 292,
        ExecuteGadgetLuaReq = 293,
        ExecuteGadgetLuaRsp = 294,
        CutSceneBeginNotify = 295,
        CutSceneFinishNotify = 296,
        CutSceneEndNotify = 297,
        ClientScriptEventNotify = 298,
        SceneEntitiesMovesReq = 299,

        SceneEntitiesMovesRsp = 300,
        EvtBeingHitNotify = 301,
        EvtAnimatorParameterNotify = 302,
        HostPlayerNotify = 303,
        EvtDoSkillSuccNotify = 304,
        EvtCreateGadgetNotify = 305,
        EvtDestroyGadgetNotify = 306,
        EvtFaceToEntityNotify = 307,
        EvtFaceToDirNotify = 308,
        EvtCostStaminaNotify = 309,
        EvtSetAttackTargetNotify = 310,
        EvtAnimatorStateChangedNotify = 311,
        EvtRushMoveNotify = 312,
        EvtBulletHitNotify = 313,
        EvtBulletDeactiveNotify = 314,
        EvtEntityStartDieEndNotify = 315,
        EvtBulletMoveNotify = 322,
        EvtAvatarEnterFocusNotify = 323,
        EvtAvatarExitFocusNotify = 324,
        EvtAvatarUpdateFocusNotify = 325,
        EntityAuthorityChangeNotify = 326,
        //BuffAddNotify = 327,
        //BuffDelNotify = 328,
        MonsterAlertChangeNotify = 329,
        MonsterForceAlertNotify = 330,
        //MonsterForceAiNotify = 331,
        AvatarEnterElementViewNotify = 332,
        TriggerCreateGadgetToEquipPartNotify = 333,
        EvtEntityRenderersChangedNotify = 334,
        AnimatorForceSetAirMoveNotify = 335,
        EvtAiSyncSkillCdNotify = 336,
        EvtBeingHitsCombineNotify = 337,
        EvtAvatarSitDownNotify = 341,
        EvtAvatarStandUpNotify = 342,
        CreateMassiveEntityReq = 343,
        CreateMassiveEntityRsp = 344,
        CreateMassiveEntityNotify = 345,
        DestroyMassiveEntityNotify = 346,
        MassiveEntityStateChangedNotify = 347,

        QuestListNotify = 401,
        QuestListUpdateNotify = 402,
        QuestDelNotify = 403,
        FinishedParentQuestNotify = 404,
        FinishedParentQuestUpdateNotify = 405,
        AddQuestContentProgressReq = 406,
        AddQuestContentProgressRsp = 407,
        GetQuestTalkHistoryReq = 408,
        GetQuestTalkHistoryRsp = 409,
        QuestCreateEntityReq = 410,
        QuestCreateEntityRsp = 411,
        QuestDestroyEntityReq = 412,
        QuestDestroyEntityRsp = 413,
        //LogTalkNotify = 414,
        //LogCutsceneNotify = 415,
        ChapterStateNotify = 416,
        QuestProgressUpdateNotify = 417,

        NpcTalkReq = 501,
        NpcTalkRsp = 502,
        GetSceneNpcPositionReq = 504,
        GetSceneNpcPositionRsp = 505,
        MetNpcIdListNotify = 506,

        PlayerStoreNotify = 601,
        StoreWeightLimitNotify = 602,
        StoreItemChangeNotify = 603,
        StoreItemDelNotify = 604,
        ItemAddHintNotify = 605,
        UseItemReq = 608,
        UseItemRsp = 609,
        DropItemReq = 610,
        DropItemRsp = 611,
        WearEquipReq = 614,
        WearEquipRsp = 615,
        TakeoffEquipReq = 616,
        TakeoffEquipRsp = 617,
        AvatarEquipChangeNotify = 618,
        WeaponUpgradeReq = 619,
        WeaponUpgradeRsp = 620,
        WeaponPromoteReq = 621,
        WeaponPromoteRsp = 622,
        ReliquaryUpgradeReq = 623,
        ReliquaryUpgradeRsp = 624,
        ReliquaryPromoteReq = 625,
        ReliquaryPromoteRsp = 626,
        AvatarCardChangeReq = 627,
        AvatarCardChangeRsp = 628,
        GrantRewardNotify = 629,
        WeaponAwakenReq = 630,
        WeaponAwakenRsp = 631,
        ItemCdGroupTimeNotify = 632,
        DropHintNotify = 633,
        CombineReq = 634,
        CombineRsp = 635,
        ForgeQueueDataNotify = 636,
        ForgeGetQueueDataReq = 637,
        ForgeGetQueueDataRsp = 638,
        ForgeStartReq = 639,
        ForgeStartRsp = 640,
        ForgeQueueManipulateReq = 641,
        ForgeQueueManipulateRsp = 642,

        GetShopReq = 701,
        GetShopRsp = 702,
        BuyGoodsReq = 703,
        BuyGoodsRsp = 704,

        GadgetInteractReq = 801,
        GadgetInteractRsp = 802,
        GadgetStateNotify = 803,
        WorktopOptionNotify = 804,
        SelectWorktopOptionReq = 805,
        SelectWorktopOptionRsp = 806,

        DungeonEntryInfoReq = 901,
        DungeonEntryInfoRsp = 902,
        PlayerEnterDungeonReq = 903,
        PlayerEnterDungeonRsp = 904,
        PlayerQuitDungeonReq = 905,
        PlayerQuitDungeonRsp = 906,
        DungeonWayPointNotify = 907,
        DungeonWayPointActivateReq = 908,
        DungeonWayPointActivateRsp = 909,
        DungeonSettleNotify = 910,
        DungeonPlayerDieNotify = 911,
        DungeonDieOptionReq = 912,
        DungeonDieOptionRsp = 913,
        DungeonShowReminderNotify = 914,
        DungeonPlayerDieReq = 915,
        DungeonPlayerDieRsp = 916,
        DungeonDataNotify = 917,
        DungeonChallengeBeginNotify = 918,
        DungeonChallengeFinishNotify = 919,
        ChallengeDataNotify = 920,
        DungeonFollowNotify = 921,
        DungeonGetStatueDropReq = 922,
        DungeonGetStatueDropRsp = 923,
        ChallengeRecordNotify = 924,
        DungeonCandidateTeamInfoNotify = 925,
        DungeonCandidateTeamInviteNotify = 926,
        DungeonCandidateTeamRefuseNotify = 927,
        DungeonCandidateTeamPlayerLeaveNotify = 928,
        DungeonCandidateTeamDismissNotify = 929,
        DungeonCandidateTeamCreateReq = 930,
        DungeonCandidateTeamCreateRsp = 931,
        DungeonCandidateTeamInviteReq = 932,
        DungeonCandidateTeamInviteRsp = 933,
        DungeonCandidateTeamKickReq = 934,
        DungeonCandidateTeamKickRsp = 935,
        DungeonCandidateTeamLeaveReq = 936,
        DungeonCandidateTeamLeaveRsp = 937,
        DungeonCandidateTeamReplyInviteReq = 938,
        DungeonCandidateTeamReplyInviteRsp = 939,
        DungeonCandidateTeamSetReadyReq = 940,
        DungeonCandidateTeamSetReadyRsp = 941,
        DungeonCandidateTeamChangeAvatarReq = 942,
        DungeonCandidateTeamChangeAvatarRsp = 943,

        UnlockAvatarTalentReq = 1001,
        UnlockAvatarTalentRsp = 1002,
        AvatarUnlockTalentNotify = 1003,
        AvatarSkillDepotChangeNotify = 1004,
        BigTalentPointConvertReq = 1005,
        BigTalentPointConvertRsp = 1006,
        AvatarSkillMaxChargeCountNotify = 1007,
        AvatarSkillInfoNotify = 1008,
        ProudSkillUpgradeReq = 1009,
        ProudSkillUpgradeRsp = 1010,
        ProudSkillChangeNotify = 1011,
        AvatarSkillUpgradeReq = 1012,
        AvatarSkillUpgradeRsp = 1013,
        AvatarSkillChangeNotify = 1014,
        ProudSkillExtraLevelNotify = 1015,

        AbilityInvocationFixedNotify = 1101,
        AbilityInvocationsNotify = 1102,
        ClientAbilityInitBeginNotify = 1103,
        ClientAbilityInitFinishNotify = 1104,
        AbilityInvocationFailNotify = 1105,
        //AvatarAbilityResetNotify = 1106,
        ClientAbilitiesInitFinishCombineNotify = 1107,
        //ElementReactionLogNotify = 1108,

        EntityPropNotify = 1201,
        LifeStateChangeNotify = 1202,
        EntityFightPropNotify = 1203,
        EntityFightPropUpdateNotify = 1204,
        AvatarFightPropNotify = 1205,
        AvatarFightPropUpdateNotify = 1206,
        EntityFightPropChangeReasonNotify = 1207,
        AvatarLifeStateChangeNotify = 1208,
        AvatarPropChangeReasonNotify = 1209,
        PlayerPropChangeReasonNotify = 1210,
        AvatarPropNotify = 1211,
        MarkNewNotify = 1212,

        MonsterSummonTagNotify = 1301,

        MailChangeNotify = 1402,
        ReadMailNotify = 1403,
        GetMailItemReq = 1404,
        GetMailItemRsp = 1405,
        DelMailReq = 1406,
        DelMailRsp = 1407,
        GetAuthkeyReq = 1408,
        GetAuthkeyRsp = 1409,
        ClientNewMailNotify = 1410,
        GetAllMailReq = 1411,
        GetAllMailRsp = 1412,

        GetGachaInfoReq = 1501,
        GetGachaInfoRsp = 1502,
        DoGachaReq = 1503,
        DoGachaRsp = 1504,

        AvatarAddNotify = 1701,
        AvatarDelNotify = 1702,
        SetUpAvatarTeamReq = 1703,
        SetUpAvatarTeamRsp = 1704,
        ChooseCurAvatarTeamReq = 1705,
        ChooseCurAvatarTeamRsp = 1706,
        ChangeAvatarReq = 1707,
        ChangeAvatarRsp = 1708,
        AvatarPromoteReq = 1709,
        AvatarPromoteRsp = 1710,
        SpringUseReq = 1711,
        SpringUseRsp = 1712,
        RefreshBackgroundAvatarReq = 1713,
        RefreshBackgroundAvatarRsp = 1714,
        AvatarTeamUpdateNotify = 1715,
        AvatarDataNotify = 1716,
        AvatarUpgradeReq = 1717,
        AvatarUpgradeRsp = 1718,
        AvatarDieAnimationEndReq = 1719,
        AvatarDieAnimationEndRsp = 1720,
        AvatarChangeElementTypeReq = 1721,
        AvatarChangeElementTypeRsp = 1722,
        AvatarFetterDataNotify = 1723,
        AvatarExpeditionDataNotify = 1724,
        AvatarExpeditionAllDataReq = 1725,
        AvatarExpeditionAllDataRsp = 1726,
        AvatarExpeditionStartReq = 1727,
        AvatarExpeditionStartRsp = 1728,
        AvatarExpeditionCallBackReq = 1729,
        AvatarExpeditionCallBackRsp = 1730,
        AvatarExpeditionGetRewardReq = 1731,
        AvatarExpeditionGetRewardRsp = 1732,
        ChangeMpTeamAvatarReq = 1734,
        ChangeMpTeamAvatarRsp = 1735,
        ChangeTeamNameReq = 1736,
        ChangeTeamNameRsp = 1737,
        SceneTeamUpdateNotify = 1738,
        //SceneTeamMPDisplayCurAvatarNotify = 1739,

        PlayerApplyEnterMpNotify = 1801,
        PlayerApplyEnterMpReq = 1802,
        PlayerApplyEnterMpRsp = 1803,
        PlayerApplyEnterMpResultNotify = 1804,
        PlayerApplyEnterMpResultReq = 1805,
        PlayerApplyEnterMpResultRsp = 1806,
        PlayerQuitFromMpNotify = 1807,

        PlayerInvestigationAllInfoNotify = 1901,
        TakeInvestigationRewardReq = 1902,
        TakeInvestigationRewardRsp = 1903,
        TakeInvestigationTargetRewardReq = 1904,
        TakeInvestigationTargetRewardRsp = 1905,
        GetInvestigationMonsterReq = 1906,
        GetInvestigationMonsterRsp = 1907,
        PlayerInvestigationNotify = 1908,
        PlayerInvestigationTargetNotify = 1909,

        SceneEntitiesMoveCombineNotify = 3001,
        UnlockTransPointReq = 3002,
        UnlockTransPointRsp = 3003,
        //PlatformRouteStateNotify = 3004,
        SceneWeatherForcastReq = 3005,
        SceneWeatherForcastRsp = 3006,
        //ExitPlayerSceneReq = 3008,
        //ExitPlayerSceneRsp = 3009,
        MarkMapReq = 3010,
        MarkMapRsp = 3011,
        AllMarkPointNotify = 3012,
        WorldDataNotify = 3013,
        EntityMoveRoomNotify = 3014,
        WorldPlayerInfoNotify = 3015,
        PostEnterSceneReq = 3016,
        PostEnterSceneRsp = 3017,
        PlayerChatReq = 3018,
        PlayerChatRsp = 3019,
        PlayerChatNotify = 3020,
        PlayerChatCDNotify = 3021,
        ChatHistoryNotify = 3022,
        SceneDataNotify = 3023,

        /// 

        ClientReportNotify = 15,
        UnionCmdNotify = 16,

        //OnCheckSegmentCRCNotify = 19,
        CheckSegmentCRCReq = 20,
        WorldPlayerRTTNotify = 21,
        EchoNotify = 22,

        PlayerLuaShellNotify = 161,

        PlayerForceExitReq = 164,

        PlayerInjectFixNotify = 166,
        TaskVarNotify = 167,

        SyncTeamEntityNotify = 348,
        DelTeamEntityNotify = 349,
        CombatInvocationsNotify = 350,
        ServerBuffChangeNotify = 351,
        EvtAiSyncCombatThreatInfoNotify = 352,
        MassiveEntityElementOpBatchNotify = 353,
        EntityAiSyncNotify = 354,

        QuestUpdateQuestVarReq = 418,
        QuestUpdateQuestVarRsp = 419,
        QuestUpdateQuestVarNotify = 420,
        QuestDestroyNpcReq = 421,
        QuestDestroyNpcRsp = 422,

        GetAllActivatedBargainDataReq = 429,
        GetAllActivatedBargainDataRsp = 430,
        ServerCondMeetQuestListUpdateNotify = 431,
        QuestGlobalVarNotify = 432,
        QuestTransmitReq = 433,
        QuestTransmitRsp = 434,
        PersonalLineAllDataReq = 435,
        PersonalLineAllDataRsp = 436,

        UnlockPersonalLineReq = 439,
        UnlockPersonalLineRsp = 440,

        ResinChangeNotify = 643,

        BuyResinReq = 649,
        BuyResinRsp = 650,
        MaterialDeleteReturnNotify = 651,
        TakeMaterialDeleteReturnReq = 652,
        TakeMaterialDeleteReturnRsp = 653,
        MaterialDeleteUpdateNotify = 654,
        McoinExchangeHcoinReq = 655,
        McoinExchangeHcoinRsp = 656,
        DestroyMaterialReq = 657,
        DestroyMaterialRsp = 658,
        SetEquipLockStateReq = 659,
        SetEquipLockStateRsp = 660,
        CalcWeaponUpgradeReturnItemsReq = 661,
        CalcWeaponUpgradeReturnItemsRsp = 662,
        ForgeDataNotify = 663,
        ForgeFormulaDataNotify = 664,
        CombineDataNotify = 665,
        CombineFormulaDataNotify = 666,

        GetDailyDungeonEntryInfoReq = 944,

        CanUseSkillNotify = 1016,
        TeamResonanceChangeNotify = 1017,

        u_LuaCodeNotify = 1110,
        AbilityChangeNotify = 1111,
        //UnionClientAbilityChangeNotify = 1112,

        FocusAvatarReq = 1740,
        FocusAvatarRsp = 1741,
        AvatarSatiationDataNotify = 1742,
        AvatarWearFlycloakReq = 1743,
        AvatarWearFlycloakRsp = 1744,
        AvatarFlycloakChangeNotify = 1745,
        AvatarGainFlycloakNotify = 1746,
        AvatarEquipAffixStartNotify = 1747,
        AvatarFetterLevelRewardReq = 1748,
        AvatarFetterLevelRewardRsp = 1749,
        AddNoGachaAvatarCardNotify = 1750,

        AllCoopInfoNotify = 1951,

        GetActivityScheduleReq = 2001,
        GetActivityScheduleRsp = 2002,
        GetActivityInfoReq = 2003,
        GetActivityInfoRsp = 2004,

        ActivityInfoNotify = 2006,
        ActivityScheduleInfoNotify = 2007,

        ServerAnnounceNotify = 2022,
        ServerAnnounceRevokeNotify = 2023,
        LoadActivityTerrainNotify = 2024,

        ReceivedTrialAvatarActivityRewardReq = 2043,
        ReceivedTrialAvatarActivityRewardRsp = 2044,

        WatcherAllDataNotify = 2201,
        WatcherChangeNotify = 2202,
        WatcherEventNotify = 2203,
        WatcherEventTypeNotify = 2204,

        PushTipsAllDataNotify = 2221,
        PushTipsChangeNotify = 2222,
        PushTipsReadFinishReq = 2223,
        PushTipsReadFinishRsp = 2224,
        GetPushTipsRewardReq = 2225,
        GetPushTipsRewardRsp = 2226,

        QueryPathReq = 2301,
        QueryPathRsp = 2302,
        ObstacleModifyNotify = 2303,
        PathfindingPingNotify = 2304,
        PathfindingEnterSceneReq = 2305,
        PathfindingEnterSceneRsp = 2306,

        TowerBriefDataNotify = 2401,
        TowerFloorRecordChangeNotify = 2402,
        TowerCurLevelRecordChangeNotify = 2403,
        TowerDailyRewardProgressChangeNotify = 2404,

        TowerTeamSelectReq = 2406,
        TowerTeamSelectRsp = 2407,
        TowerAllDataReq = 2408,
        TowerAllDataRsp = 2409,

        TowerEnterLevelReq = 2411,
        TowerEnterLevelRsp = 2412,
        TowerBuffSelectReq = 2413,
        TowerBuffSelectRsp = 2414,

        TowerSurrenderReq = 2421,
        TowerSurrenderRsp = 2422,
        TowerGetFloorStarRewardReq = 2423,
        TowerGetFloorStarRewardRsp = 2424,

        TowerLevelEndNotify = 2430,
        TowerLevelStarCondNotify = 2431,
        TowerMiddleLevelChangeTeamNotify = 2432,

        OpActivityStateNotify = 2501,

        SignInInfoReq = 2503,
        SignInInfoRsp = 2504,
        GetSignInRewardReq = 2505,
        GetSignInRewardRsp = 2506,

        BonusActivityUpdateNotify = 2512,
        BonusActivityInfoReq = 2513,
        BonusActivityInfoRsp = 2514,
        GetBonusActivityRewardReq = 2515,
        GetBonusActivityRewardRsp = 2516,

        BattlePassAllDataNotify = 2601,
        BattlePassMissionUpdateNotify = 2602,
        BattlePassMissionDelNotify = 2603,
        BattlePassCurScheduleUpdateNotify = 2604,
        TakeBattlePassRewardReq = 2605,
        TakeBattlePassRewardRsp = 2606,
        TakeBattlePassMissionPointReq = 2607,
        TakeBattlePassMissionPointRsp = 2608,
        GetBattlePassProductReq = 2609,
        GetBattlePassProductRsp = 2610,

        SetBattlePassViewedReq = 2613,
        SetBattlePassViewedRsp = 2614,
        BattlePassBuySuccNotify = 2615,
        BuyBattlePassLevelReq = 2616,
        BuyBattlePassLevelRsp = 2617,

        AchievementAllDataNotify = 2651,
        AchievementUpdateNotify = 2652,
        TakeAchievementRewardReq = 2653,
        TakeAchievementRewardRsp = 2654,
        TakeAchievementGoalRewardReq = 2655,
        TakeAchievementGoalRewardRsp = 2656,

        GetBlossomBriefInfoListReq = 2701,
        GetBlossomBriefInfoListRsp = 2702,
        BlossomBriefInfoNotify = 2703,
        WorldOwnerBlossomBriefInfoNotify = 2704,
        WorldOwnerBlossomScheduleInfoNotify = 2705,

        GetCityReputationInfoReq = 2804,
        GetCityReputationInfoRsp = 2805,
        TakeCityReputationParentQuestReq = 2806,
        TakeCityReputationParentQuestRsp = 2807,
        AcceptCityReputationRequestReq = 2808,
        AcceptCityReputationRequestRsp = 2809,
        CancelCityReputationRequestReq = 2810,
        CancelCityReputationRequestRsp = 2811,
        GetCityReputationMapInfoReq = 2812,
        GetCityReputationMapInfoRsp = 2813,
        TakeCityReputationExploreRewardReq = 2814,
        TakeCityReputationExploreRewardRsp = 2815,
        CityReputationDataNotify = 2816,

        PlayerOfferingDataNotify = 2901,
        PlayerOfferingReq = 2902,
        PlayerOfferingRsp = 2903,
        TakeOfferingLevelRewardReq = 2904,
        TakeOfferingLevelRewardRsp = 2905,

        UnfreezeGroupLimitNotify = 3037,

        SetEntityClientDataNotify = 3041,
        GroupSuiteNotify = 3042,
        GroupUnloadNotify = 3043,
        MonsterAIConfigHashNotify = 3044,

        ShowTemplateReminderNotify = 3046,
        ShowCommonTipsNotify = 3047,
        CloseCommonTipsNotify = 3048,
        ChangeWorldToSingleModeNotify = 3049,
        SyncScenePlayTeamEntityNotify = 3050,
        DelScenePlayTeamEntityNotify = 3051,
        PlayerEyePointStateNotify = 3052,
        GetMapMarkTipsReq = 3053,
        GetMapMarkTipsRsp = 3054,
        ChangeWorldToSingleModeReq = 3055,
        ChangeWorldToSingleModeRsp = 3056,
        GetWorldMpInfoReq = 3057,
        GetWorldMpInfoRsp = 3058,
        EntityConfigHashNotify = 3059,

        PlayerRoutineDataNotify = 3501,
        WorldAllRoutineTypeNotify = 3502,
        WorldRoutineTypeRefreshNotify = 3503,
        WorldRoutineChangeNotify = 3504,
        WorldRoutineTypeCloseNotify = 3505,

        GetPlayerFriendListReq = 4001,
        GetPlayerFriendListRsp = 4002,

        AskAddFriendReq = 4005,
        AskAddFriendRsp = 4006,
        DealAddFriendReq = 4007,
        DealAddFriendRsp = 4008,
        GetPlayerSocialDetailReq = 4009,
        GetPlayerSocialDetailRsp = 4010,
        DeleteFriendReq = 4011,
        DeleteFriendRsp = 4012,
        SetPlayerBirthdayReq = 4013,
        SetPlayerBirthdayRsp = 4014,
        SetPlayerSignatureReq = 4015,
        SetPlayerSignatureRsp = 4016,
        SetPlayerHeadImageReq = 4017,
        SetPlayerHeadImageRsp = 4018,
        UpdatePS4FriendListNotify = 4019,
        DeleteFriendNotify = 4020,
        AddFriendNotify = 4021,
        AskAddFriendNotify = 4022,
        SetNameCardReq = 4023,
        SetNameCardRsp = 4024,
        GetAllUnlockNameCardReq = 4025,
        GetAllUnlockNameCardRsp = 4026,
        AddBlacklistReq = 4027,
        AddBlacklistRsp = 4028,
        RemoveBlacklistReq = 4029,
        RemoveBlacklistRsp = 4030,
        UnlockNameCardNotify = 4031,
        GetRecentMpPlayerListReq = 4032,
        GetRecentMpPlayerListRsp = 4033,
        SocialDataNotify = 4034,
        TakeFirstShareRewardReq = 4035,
        TakeFirstShareRewardRsp = 4036,
        UpdatePS4BlockListReq = 4037,
        UpdatePS4BlockListRsp = 4038,
        GetPlayerBlacklistReq = 4039,
        GetPlayerBlacklistRsp = 4040,
        PlayerReportReq = 4041,
        PlayerReportRsp = 4042,

        RechargeReq = 4101,
        RechargeRsp = 4102,
        OrderFinishNotify = 4103,
        CardProductRewardNotify = 4104,
        PlayerRechargeDataNotify = 4105,
        OrderDisplayNotify = 4106,
        ReportTrackingIOInfoNotify = 4107,

        PlayerStartMatchReq = 4151,
        PlayerStartMatchRsp = 4152,
        PlayerMatchInfoNotify = 4153,

        CodexDataFullNotify = 4201,
        CodexDataUpdateNotify = 4202,
        QueryCodexMonsterBeKilledNumReq = 4203,
        QueryCodexMonsterBeKilledNumRsp = 4204,

        ViewCodexReq = 4207,
        ViewCodexRsp = 4208,

        CoopDataNotify = 1957,

        OpActivityDataNotify = 5103, 

        AnchorPointDataNotify = 4251,
        UseAnchorPointWidgetReq = 4252,
        UseAnchorPointWidgetRsp = 4253,
        AnchorPointOpReq = 4254,
        AnchorPointOpRsp = 4255,
        UseBonfireWidgetReq = 4256,
        UseBonfireWidgetRsp = 4257,
        SetUpLunchBoxWidgetReq = 4258,
        SetUpLunchBoxWidgetRsp = 4259,
        QuickUseWidgetReq = 4260,
        QuickUseWidgetRsp = 4261,
        WidgetCoolDownNotify = 4262,
        WidgetReportReq = 4263,
        WidgetReportRsp = 4264,
        ClientCollectorDataNotify = 4265,
        OneoffGatherPointDetectorDataNotify = 4266,

        //SetQuickUseWidgetReq = 4298,
        //SetQuickUseWidgetRsp = 4299,
        AllWidgetDataNotify = 4300,

        TakeHuntingOfferReq = 4301,
        TakeHuntingOfferRsp = 4302,

        ScenePlayBattleInfoNotify = 4351,
        ScenePlayOwnerCheckReq = 4352,
        ScenePlayOwnerCheckRsp = 4353,
        ScenePlayOwnerStartInviteReq = 4354,
        ScenePlayOwnerStartInviteRsp = 4355,
        ScenePlayOwnerInviteNotify = 4356,
        ScenePlayGuestReplyInviteReq = 4357,
        ScenePlayGuestReplyInviteRsp = 4358,
        ScenePlayGuestReplyNotify = 4359,
        ScenePlayInviteResultNotify = 4360,
        ScenePlayInfoListNotify = 4361,
        ScenePlayBattleInterruptNotify = 4362,
        ScenePlayBattleResultNotify = 4363,
        ScenePlayBattleUidOpNotify = 4364,
        ScenePlayBattleInfoListNotify = 4365,
        ScenePlayOutofRegionNotify = 4366,

        PrivateChatReq = 4951,
        PrivateChatRsp = 4952,
        PrivateChatNotify = 4953,
        PrivateChatSetSequenceReq = 4954,
        PrivateChatSetSequenceRsp = 4955,

        PullPrivateChatReq = 4956,
        PullPrivateChatRsp = 4957,
        PullRecentChatReq = 4958,
        PullRecentChatRsp = 4959,
        ReadPrivateChatReq = 4960,
        ReadPrivateChatRsp = 4961,

        ReunionBriefInfoReq = 5051,
        ReunionBriefInfoRsp = 5052,
        RegionSearchNotify = 5601,

    }
}