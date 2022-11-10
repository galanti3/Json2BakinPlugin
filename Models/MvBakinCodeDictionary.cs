using Json2BakinPlugin.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Json2BakinPlugin.Properties.Resources;

namespace Json2BakinPlugin.Models
{

	public static class MvBakinCodeDictionary
	{
		#region Method
		public static List<string> Code(int code)
		{
			Dictionary<int, List<string>> codePairs = new Dictionary<int, List<string>>() {
			{00000, new List<string>{"CLOSE", Dic_Close}},
			{10100, new List<string>{"DIALOGUE", Dic_Dialogue}},
			{10101, new List<string>{"MESSAGE", Dic_Message}},
			{10200, new List<string>{"CHOICES", Dic_Choices}},
			{10300, new List<string>{"CHANGE_STRING_VARIABLE", Dic_ChangeStringVariable}},
			{10400, new List<string>{"ITEMMENU", Dic_ItemMenu}},
			{10500, new List<string>{"TELOP", Dic_Telop}},
			{10800, new List<string>{"COMMENT", Dic_Comment}},
			{10900, new List<string>{"ERROR", Dic_Skip}}, //comment out
			{11100, new List<string>{"IFSWITCH", Dic_IfSwitch}},
			{11101, new List<string>{"IFVARIABLE", Dic_IfVariable}},
			{11102, new List<string>{"IFSWITCH", Dic_IfVariable}},
			{11103, new List<string>{"IFVARIABLE", Dic_IfTimer}},
			{11104, new List<string>{"IFPARTY", Dic_IfParty}},
			{11105, new List<string>{"IF_STRING_VARIABLE", Dic_IfStringVariable}},
			{11106, new List<string>{"ERROR_IF", Dic_IfClassSkillState}}, //comment out
			{11107, new List<string>{"IFITEM", Dic_IfItem}},
			{11108, new List<string>{"BTL_IFMONSTER", Dic_BtlIfMonster}},
			{11109, new List<string>{"ERROR_IF", Dic_IfDirection}}, //comment out
			{11110, new List<string>{"IFMONEY", Dic_IfMoney}},
			{11111, new List<string>{"IFITEM", Dic_IfPartyItem}},
			{11112, new List<string>{"IFITEM", Dic_IfPartyWeapon}},
			{11113, new List<string>{"IFITEM", Dic_IfPartyArmor}},
			{11114, new List<string>{"ERROR_IF", Dic_IfButtonPressed}}, //comment out
			{11115, new List<string>{"ERROR_IF", Dic_IfScript}}, //comment out
			{11116, new List<string>{"ERROR_IF", Dic_IfVehicle}}, //comment out
			{11200, new List<string>{"LOOP", Dic_Loop}},
			{11300, new List<string>{"BREAK", Dic_LoopBreak}},
			{11500, new List<string>{"END", Dic_End}},
			{11700, new List<string>{"EXEC", Dic_Exec}},
			{11800, new List<string>{"ERROR_WITH_CONTENT", Dic_Label}}, //comment out
			{11900, new List<string>{"ERROR_WITH_CONTENT", Dic_LabelJump}}, //comment out
			{12100, new List<string>{"SWITCH", Dic_Switch}},
			{12200, new List<string>{"HLVARIABLE", Dic_HlVariable}},
			{12201, new List<string>{"HLVARIABLE", Dic_VarRandom}},
			{12202, new List<string>{"HLVARIABLE", Dic_VarItem}},
			{12203, new List<string>{"HLVARIABLE", Dic_VarActor}},
			{12204, new List<string>{"HLVARIABLE", Dic_VarEnemy}},
			{12205, new List<string>{"HLVARIABLE", Dic_VarChar}},
			{12206, new List<string>{"ERROR", Dic_VarPartyActorId}}, //comment out
			{12207, new List<string>{"HLVARIABLE", Dic_VarGameData}},
			{12208, new List<string>{"ERROR_WITH_CONTENT", Dic_VarScript}}, //comment out
			{12300, new List<string>{"SWITCH", Dic_SelfSwitch}},
			{12400, new List<string>{"TIMER", Dic_Timer}}, //custom command
			{12500, new List<string>{"MONEY", Dic_Money}},
			{12600, new List<string>{"ITEM", Dic_Item}},
			{12700, new List<string>{"ITEM", Dic_Weapon}},
			{12800, new List<string>{"ITEM", Dic_Armor}},
			{12900, new List<string>{"PARTY", Dic_Party}},
			{13200, new List<string>{"ERROR", Dic_BtlBgm}}, //comment out
			{13300, new List<string>{"ERROR", Dic_WinMe}}, //comment out
			{13400, new List<string>{"SW_SAVE", Dic_Save}},
			{13500, new List<string>{"SW_MENU", Dic_Menu}},
			{13600, new List<string>{"SW_ENCOUNTING", Dic_Encount}},
			{13700, new List<string>{"ERROR", Dic_PartyOrder}}, //comment out
			{13800, new List<string>{"CHANGE_LAYOUT", Dic_WindowColor}},
			{13900, new List<string>{"ERROR", Dic_LostMe}}, //comment out
			{14000, new List<string>{"ERROR", Dic_VehicleBgm}}, //comment out
			{20100, new List<string>{"PLMOVE", Dic_PlayerMove}},
			{20200, new List<string>{"ERROR", Dic_VehiclePosition}}, //comment out
			{20300, new List<string>{"MOVE", Dic_EventMove}},
			{20400, new List<string>{"CAM_ANIMATION", Dic_SetCamera}},
			{20500, new List<string>{"MOVEROUTE", Dic_MoveRoute}},
			{20600, new List<string>{"ERROR", Dic_VehicleRide}}, //comment out
			{21100, new List<string>{"EVHIDE", Dic_EventTransparent}},
			{21200, new List<string>{"EFFECT", Dic_Effect}},
			{21300, new List<string>{"EMOTE", Dic_Emote}},
			{21400, new List<string>{"DESTROY_EVENT", Dic_DestroyEvent}},
			{21600, new List<string>{"WALK_IN_ROWS", Dic_WalkInRows}},
			{21700, new List<string>{"ERROR", Dic_Gather}}, //comment out
			{22100, new List<string>{"SCREEN_FADE", Dic_Fadeout}},
			{22200, new List<string>{"SCREEN_FADE", Dic_Fadein}},
			{22300, new List<string>{"SCREEN_COLOR", Dic_ScreenColor}},
			{22400, new List<string>{"SCREEN_FLASH", Dic_Flash}},
			{22500, new List<string>{"SCREEN_SHAKE", Dic_Shake}},
			{23000, new List<string>{"WAIT", Dic_Wait}},
			{23100, new List<string>{"SPPICTURE", Dic_ShowImage}},
			{23200, new List<string>{"SPMOVE", Dic_MoveImage}},
			{23300, new List<string>{"ERROR", Dic_RotateImage}}, //comment out
			{23400, new List<string>{"ERROR", Dic_ChangeImageColor}}, //comment out
			{23500, new List<string>{"SPHIDE", Dic_DeleteImage}},
			{23600, new List<string>{"CHANGE_RENDER", Dic_ChangeRender}},
			{24100, new List<string>{"PLAYBGM", Dic_PlayBgm}},
			{24200, new List<string>{"PLAYBGM", Dic_FadeoutBgm}},
			{24300, new List<string>{"ERROR", Dic_SaveBgm}}, //comment out
			{24400, new List<string>{"ERROR", Dic_ResumeBgm}}, //comment out
			{24500, new List<string>{"PLAYBGS", Dic_PlayBgs}},
			{24600, new List<string>{"PLAYBGS", Dic_FadeoutBgs}},
			{24900, new List<string>{"PLAYJINGLE", Dic_PlayJingle}},
			{25000, new List<string>{"PLAYSE", Dic_PlaySe}},
			{25100, new List<string>{"STOPSE", Dic_StopSe}},
			{26100, new List<string>{"PLAYMOVIE", Dic_PlayMovie}},
			{28100, new List<string>{"ERROR", Dic_MapName}}, //comment out
			{28200, new List<string>{"ERROR", Dic_Tileset}}, //comment out
			{28300, new List<string>{"CHANGE_RENDER", Dic_BtlBack}},
			{28400, new List<string>{"CHANGE_RENDER", Dic_Back}},
			{28500, new List<string>{"GET_TERRAIN", Dic_Terrain}},
			{30100, new List<string>{"BOSSBATTLE", Dic_BossBattle}},
			{30200, new List<string>{"SHOP", Dic_Shop}},
			{30300, new List<string>{"CHANGE_HERO_NAME", Dic_HeroName}},
			{31100, new List<string>{"CHG_HPMP", Dic_Hp}},
			{31200, new List<string>{"CHG_HPMP", Dic_Mp}},
			{31300, new List<string>{"CHG_STTAILM", Dic_State}},
			{31400, new List<string>{"FULLRECOV", Dic_FullRecover}},
			{31500, new List<string>{"CHG_EXP", Dic_Exp}},
			{31600, new List<string>{"ERROR", Dic_Level}}, //comment out
			{31700, new List<string>{"STATUS", Dic_Status}},
			{31800, new List<string>{"CHG_SKILL", Dic_Skill}},
			{31900, new List<string>{"EQUIP", Dic_Equip}},
			{32000, new List<string>{"STRING_VARIABLE", Dic_StringVariable}},
			{32100, new List<string>{"CHANGE_JOB", Dic_Job}},
			{32200, new List<string>{"PLGRAPHIC", Dic_CastGraphic}},
			{32300, new List<string>{"ERROR", Dic_VehicleGraphic}}, //comment out
			{32400, new List<string>{"ERROR", Dic_Nickname}}, //comment out
			{32500, new List<string>{"ERROR", Dic_Profile}}, //comment out
			{32600, new List<string>{"ERROR", Dic_Tp}}, //comment out
			{33100, new List<string>{"BTL_HEAL", Dic_BtlCastHp}},
			{33200, new List<string>{"BTL_HEAL", Dic_BtlCastMp}},
			{34200, new List<string>{"ERROR", Dic_EnemyTp}}, //comment out
			{33300, new List<string>{"BTL_STATUS", Dic_BtlCastState}},
			{33400, new List<string>{"ERROR", Dic_EnemyFullRecover}}, //comment out
			{33500, new List<string>{"BTL_APPEAR", Dic_Emerge}},
			{33600, new List<string>{"ERROR", Dic_Transform }}, //comment out
			{33700, new List<string>{"EFFECT", Dic_BtlCastEffect}},
			{33900, new List<string>{"BTL_ACTION", Dic_BtlCastStatus}},
			{34000, new List<string>{"BTL_STOP", Dic_BtlEnd}},
			{35100, new List<string>{"SHOW_SCORE_BOARD", Dic_ShowMenu}},
			{35200, new List<string>{"SAVE", Dic_ShowSave}},
			{35300, new List<string>{"ERROR", Dic_GameOver}}, //comment out
			{35400, new List<string>{"ERROR", Dic_ToTitle}}, //comment out
			{35500, new List<string>{"ERROR_WITH_CONTENT", Dic_Script}}, //comment out
			{35600, new List<string>{"ERROR_WITH_CONTENT", Dic_Plugin}}, //comment out
			{35700, new List<string>{"ERROR_WITH_CONTENT", Dic_PluginCont}}, //comment out
			{40200, new List<string>{"BRANCH", Dic_Choice}},
			{40300, new List<string>{"BRANCH", Dic_ChoiceCancel}},
            {40400, new List<string>{"CLOSE", Dic_ChoicesEnd}},
            {41100, new List<string>{"ELSE", Dic_Else}},
			{41300, new List<string>{"ENDLOOP", Dic_EndLoop}},
			{60200, new List<string>{"ELSE", Dic_IfEscape}},
			{60300, new List<string>{"ELSE", Dic_IfLost}},

			//route. currently player or this-event only..
			{0100, new List<string>{"PLWALK", Dic_Player + Dic_MoveDown}}, //MOVE_DOWN
			{0200, new List<string>{"PLWALK", Dic_Player + Dic_MoveLeft}}, //MOVE_LEFT
			{0300, new List<string>{"PLWALK", Dic_Player + Dic_MoveRight}}, //MOVE_RIGHT
			{0400, new List<string>{"PLWALK", Dic_Player + Dic_MoveUp}}, //MOVE_UP
			{0500, new List<string>{"PLWALK", Dic_Player + Dic_MoveLD}}, //MOVE_LOWER_L
			{0600, new List<string>{"PLWALK", Dic_Player + Dic_MoveRD}}, //MOVE_LOWER_R
			{0700, new List<string>{"PLWALK", Dic_Player + Dic_MoveLU}}, //MOVE_UPPER_L
			{0800, new List<string>{"PLWALK", Dic_Player + Dic_MoveRU}}, //MOVE_UPPER_R
			{0900, new List<string>{"PLWALK", Dic_Player + Dic_MoveRandom}}, //MOVE_RANDOM
			{1000, new List<string>{"PLWALK", Dic_Player + Dic_Toward}}, //MOVE_TOWARD
			{1100, new List<string>{"PLWALK", Dic_Player + Dic_Away}}, //MOVE_AWAY
			{1200, new List<string>{"PLWALK", Dic_Player + Dic_Forward}}, //MOVE_FORWARD
			{1300, new List<string>{"PLWALK", Dic_Player + Dic_Backward}}, //MOVE_BACKWARD
			{1400, new List<string>{"ADDFORCEPL", Dic_Player + Dic_Jump}}, //JUMP
			{1500, new List<string>{"WAIT", Dic_Wait}}, //WAIT
			{1600, new List<string>{"PLROTATE", Dic_Player + Dic_TurnDown}}, //TURN_DOWN
			{1700, new List<string>{"PLROTATE", Dic_Player + Dic_TurnLeft}}, //TURN_LEFT
			{1800, new List<string>{"PLROTATE", Dic_Player + Dic_TurnRight}}, //TURN_RIGHT
			{1900, new List<string>{"PLROTATE", Dic_Player + Dic_TurnUp}}, //TURN_UP
			{2000, new List<string>{"PLROTATE", Dic_Player + Dic_RotRight90}}, //TURN_90D_R
			{2100, new List<string>{"PLROTATE", Dic_Player + Dic_RotLeft90}}, //TURN_90D_L
			{2200, new List<string>{"PLROTATE", Dic_Player + Dic_Rot180}}, //TURN_180D
			{2300, new List<string>{"PLROTATE", Dic_Player + Dic_RotLeftRight90}}, //TURN_90D_R_L
			{2400, new List<string>{"PLROTATE", Dic_Player + Dic_RotRandom}}, //TURN_RANDOM
			{2500, new List<string>{"PLROTATE", Dic_Player + Dic_TurnToward}}, //TURN_TOWARD
			{2600, new List<string>{"PLROTATE", Dic_Player + Dic_TurnAway}}, //TURN_AWAY
			{2700, new List<string>{"SWITCH", Dic_Player + Dic_SwitchOn}}, //SWITCH_ON
			{2800, new List<string>{"SWITCH", Dic_Player + Dic_SwitchOFF}}, //SWITCH_OFF
			{2900, new List<string>{"PLWALKSPEED", Dic_Player + Dic_MoveSpeed}}, //CHANGE_SPEED
			{3000, new List<string>{"ERROR", Dic_Player + Dic_MoveFrequency}},	//CHANGE_FREQ
			{3100, new List<string>{"GRAPHIC", Dic_Player + Dic_WalkAnimeOn}}, //WALK_ANIME_ON
			{3200, new List<string>{"GRAPHIC", Dic_Player + Dic_WaklAnimeOff}}, //WALK_ANIME_OFF
			{3300, new List<string>{"PLMOTION", Dic_Player + Dic_StepAnimeOn}}, //STEP_ANIME_ON
			{3400, new List<string>{"PLMOTION", Dic_Player + Dic_StepAnimeOff}}, //STEP_ANIME_OFF
			{3500, new List<string>{"SW_PLLOCKROTATE", Dic_Player + Dic_DirFixOn}}, //DIR_FIX_ON
			{3600, new List<string>{"SW_PLLOCKROTATE", Dic_Player + Dic_DirFixOff}}, //DIR_FIX_OFF
			{3700, new List<string>{"CHANGE_PLAYER_MOVABLE", Dic_Player + Dic_ThroughOn}}, //THROUGH_ON
			{3800, new List<string>{"CHANGE_PLAYER_MOVABLE", Dic_Player + Dic_ThrougnOff}}, //THROUGH_OFF
			{3900, new List<string>{"PLHIDE", Dic_Player + Dic_TransOn}}, //TRANSPARENT_ON
			{4000, new List<string>{"PLHIDE", Dic_Player + Dic_TransOff}}, //TRANSPARENT_OFF
			{4100, new List<string>{"GRAPHIC", Dic_Player + Dic_CharChip}}, //CHANGE_IMAGE
			{4200, new List<string>{"ERROR", Dic_Player + Dic_Opacity}}, //CHANGE_OPACITY
			{4300, new List<string>{"ERROR", Dic_Player + Dic_MixChip}}, //CHANGE_BLEND_MODE
			{4400, new List<string>{"PLAYSE", Dic_Player + Dic_PlaySe2}}, //PLAY_SE
			{4500, new List<string>{"ERROR_WITH_CONTENT", Dic_Player + Dic_Script2}}, //SCRIPT
			{5000, new List<string>{"WALK_TGT", Dic_PlWalkTgt}}, //Move to destination

			{0101, new List<string>{"WALK", Dic_Event + Dic_MoveDown}}, //MOVE_DOWN
			{0201, new List<string>{"WALK", Dic_Event + Dic_MoveLeft}}, //MOVE_LEFT
			{0301, new List<string>{"WALK", Dic_Event + Dic_MoveRight}}, //MOVE_RIGHT
			{0401, new List<string>{"WALK", Dic_Event + Dic_MoveUp}}, //MOVE_UP
			{0501, new List<string>{"WALK", Dic_Event + Dic_MoveLD}}, //MOVE_LOWER_L
			{0601, new List<string>{"WALK", Dic_Event + Dic_MoveRD}}, //MOVE_LOWER_R
			{0701, new List<string>{"WALK", Dic_Event + Dic_MoveLU}}, //MOVE_UPPER_L
			{0801, new List<string>{"WALK", Dic_Event + Dic_MoveRU}}, //MOVE_UPPER_R
			{0901, new List<string>{"WALK", Dic_Event + Dic_MoveRandom}}, //MOVE_RANDOM
			{1001, new List<string>{"WALK", Dic_Event + Dic_Toward}}, //MOVE_TOWARD
			{1101, new List<string>{"WALK", Dic_Event + Dic_Away}}, //MOVE_AWAY
			{1201, new List<string>{"WALK", Dic_Event + Dic_Forward}}, //MOVE_FORWARD
			{1301, new List<string>{"WALK", Dic_Event + Dic_Backward}}, //MOVE_BACKWARD
			{1401, new List<string>{"ADDFORCE", Dic_Event + Dic_Jump}}, //JUMP
			{1601, new List<string>{"ROTATE", Dic_Event + Dic_TurnDown}}, //TURN_DOWN
			{1701, new List<string>{"ROTATE", Dic_Event + Dic_TurnLeft}}, //TURN_LEFT
			{1801, new List<string>{"ROTATE", Dic_Event + Dic_TurnRight}}, //TURN_RIGHT
			{1901, new List<string>{"ROTATE", Dic_Event + Dic_TurnUp}}, //TURN_UP
			{2001, new List<string>{"ROTATE", Dic_Event + Dic_RotRight90}}, //TURN_90D_R
			{2101, new List<string>{"ROTATE", Dic_Event + Dic_RotLeft90}}, //TURN_90D_L
			{2201, new List<string>{"ROTATE", Dic_Event + Dic_Rot180}}, //TURN_180D
			{2301, new List<string>{"ROTATE", Dic_Event + Dic_RotLeftRight90}}, //TURN_90D_R_L
			{2401, new List<string>{"ROTATE", Dic_Event + Dic_RotRandom}}, //TURN_RANDOM
			{2501, new List<string>{"ROTATE", Dic_Event + Dic_TurnToward}}, //TURN_TOWARD
			{2601, new List<string>{"ROTATE", Dic_Event + Dic_TurnAway}}, //TURN_AWAY
			{2701, new List<string>{"SWITCH", Dic_SwitchOn}}, //SWITCH_ON
			{2801, new List<string>{"SWITCH", Dic_SwitchOFF}}, //SWITCH_OFF
			{2901, new List<string>{"WALKSPEED", Dic_Event + Dic_MoveSpeed}}, //CHANGE_SPEED
			{3001, new List<string>{"ERROR", Dic_Event + Dic_MoveFrequency}},	//CHANGE_FREQ
			{3101, new List<string>{"GRAPHIC", Dic_Event + Dic_WalkAnimeOn}}, //WALK_ANIME_ON
			{3201, new List<string>{"GRAPHIC", Dic_Event + Dic_WaklAnimeOff}}, //WALK_ANIME_OFF
			{3301, new List<string>{"MOTION", Dic_Event + Dic_StepAnimeOn}}, //STEP_ANIME_ON
			{3401, new List<string>{"MOTION", Dic_Event + Dic_StepAnimeOff}}, //STEP_ANIME_OFF
			{3501, new List<string>{"SW_LOCKROTATE", Dic_Event + Dic_DirFixOn}}, //DIR_FIX_ON
			{3601, new List<string>{"SW_LOCKROTATE", Dic_Event + Dic_DirFixOff}}, //DIR_FIX_OFF
			{3701, new List<string>{"CHANGE_MOVABLE", Dic_Event + Dic_ThroughOn}}, //THROUGH_ON
			{3801, new List<string>{"CHANGE_MOVABLE", Dic_Event + Dic_ThrougnOff}}, //THROUGH_OFF
			{3901, new List<string>{"EVHIDE", Dic_Event + Dic_TransOn}}, //TRANSPARENT_ON
			{4001, new List<string>{"EVHIDE", Dic_Event + Dic_TransOff}}, //TRANSPARENT_OFF
			{4101, new List<string>{"GRAPHIC", Dic_Event + Dic_CharChip}}, //CHANGE_IMAGE
			{4201, new List<string>{"ERROR", Dic_Event + Dic_Opacity}}, //CHANGE_OPACITY
			{4301, new List<string>{"ERROR", Dic_Event + Dic_MixChip}}, //CHANGE_BLEND_MODE
			{5001, new List<string>{"WALK_TGT", Dic_EvWalkTgt}}, //Move to destination

            //commands not used in MV
            {1, new List<string>{"SPTEXT", Dic_SpText}},
            {3, new List<string>{"CHANGE_GAMEOVER_ACTION", Dic_ChangeGameoverAction}},
            {4, new List<string>{"WEBBROWSER", Dic_WebBrowser}},
            {5, new List<string>{"SW_CAMLOCK", Dic_SwCamLock}},
            {6, new List<string>{"BTL_SW_CAMERA", Dic_BtlSwCamera}},
            {8, new List<string>{"CHANGE_PLAYER_SCALE", Dic_ChangePlayerScale}},
            {9, new List<string>{"JOINT_WEAPON", Dic_JointWeapon}},
            {11, new List<string>{"INVINCIBLE", Dic_Invincible}},
            {12, new List<string>{"PLSNAP", Dic_PlSnap}},
            {13, new List<string>{"PLWALK_TGT", Dic_PlWalkTgt}},
            {14, new List<string>{"ROTATEPL_XYZ", Dic_RotatePlXyz}},
            {15, new List<string>{"WALK_IN_ROWS_ORDER", Dic_WalkInRowsOrder}},
            {16, new List<string>{"PLSUBGRP", Dic_PlSubgrp}},
            {17, new List<string>{"ITEM_THROW_OUT", Dic_ItemThrowOut}},
            {19, new List<string>{"CHANGE_SCALE", Dic_ChangeScale}},
            {20, new List<string>{"EVSNAP", Dic_EvSnap}},
            {21, new List<string>{"EVWALK_TGT", Dic_EvWalkTgt}},
            {22, new List<string>{"ROTATE_XYZ", Dic_RotateXyz}},
            {24, new List<string>{"SUBGRP", Dic_Subgrp}},
            {26, new List<string>{"HLSTRVARIABLE", Dic_HlStrVariable}},
            {27, new List<string>{"REPLACE_STRING_VARIABLE", Dic_ReplaceStringVariable}},
            {28, new List<string>{"SW_PLLOCK", Dic_SwPlLock}},
            {29, new List<string>{"SW_DASH", Dic_SwDash}},
            {30, new List<string>{"SW_JUMP", Dic_SwJump}},
            {31, new List<string>{"INN", Dic_Inn}},
            {32, new List<string>{"SHOT_EVENT", Dic_ShotEvent}},
            {33, new List<string>{"EXIT", Dic_Exit}},
            {34, new List<string>{"FACEEMOTION", Dic_FaceMotion}},
            {35, new List<string>{"IF_INVENTORY_EMPTY", Dic_IfInventoryEmpty}},
            {36, new List<string>{"BTL_IFBATTLE", Dic_BtlIfBattle}},
            {37, new List<string>{"COL_CONTACT", Dic_ColContack}},
            {38, new List<string>{"COL_RAYCAST", Dic_ColRaycast}},
            {39, new List<string>{"CHANGE_PLAYER_HEIGHT", Dic_ChangePlayerHeight}},
            {40, new List<string>{"FALL_PLAYER", Dic_FallPlayer}},
            {41, new List<string>{"CHANGE_HEIGHT", Dic_ChangeHeight}},
            {42, new List<string>{"FALL_EVENT", Dic_FallEvent}},
            };

            return codePairs.Where(d => d.Key == code).FirstOrDefault().Value;
        }
        #endregion
    }
}
