using Json2BakinPlugin.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
{

	public static class MvBakinCodeDictionary
	{
		#region Method
		public static List<string> Code(int code)
		{
			Dictionary<int, List<string>> codePairs = new Dictionary<int, List<string>>() {
			{00000, new List<string>{"CLOSE", Resources.Dic_Close}},
			{10100, new List<string>{"DIALOGUE", Resources.Dic_Dialogue}},
			{10101, new List<string>{"MESSAGE", Resources.Dic_Message}},
			{10200, new List<string>{"CHOICES", Resources.Dic_Choices}},
			{10300, new List<string>{"CHANGE_STRING_VARIABLE", Resources.Dic_ChangeStringVariable}},
			{10400, new List<string>{"ITEMMENU", Resources.Dic_ItemMenu}},
			{10500, new List<string>{"TELOP", Resources.Dic_Telop}},
			{10800, new List<string>{"COMMENT", Resources.Dic_Comment}},
			{10900, new List<string>{"ERROR", Resources.Dic_Skip}}, //comment out
			{11100, new List<string>{"IFSWITCH", Resources.Dic_IfSwitch}},
			{11101, new List<string>{"IFVARIABLE", Resources.Dic_IfVariable}},
			{11102, new List<string>{"IFSWITCH", Resources.Dic_IfVariable}},
			{11103, new List<string>{"ERROR_IF", Resources.Dic_IfTimer}}, //comment out
			{11104, new List<string>{"IFPARTY", Resources.Dic_IfParty}},
			{11105, new List<string>{"IF_STRING_VARIABLE", Resources.Dic_IfStringVariable}},
			{11106, new List<string>{"ERROR_IF", Resources.Dic_IfClassSkillState}}, //comment out
			{11107, new List<string>{"IFITEM", Resources.Dic_IfItem}},
			{11108, new List<string>{"BTL_IFMONSTER", Resources.Dic_BtlIfMonster}},
			{11109, new List<string>{"ERROR_IF", Resources.Dic_IfDirection}}, //comment out
			{11110, new List<string>{"IFMONEY", Resources.Dic_IfMoney}},
			{11111, new List<string>{"IFITEM", Resources.Dic_IfPartyItem}},
			{11112, new List<string>{"IFITEM", Resources.Dic_IfPartyWeapon}},
			{11113, new List<string>{"IFITEM", Resources.Dic_IfPartyArmor}},
			{11114, new List<string>{"ERROR_IF", Resources.Dic_IfButtonPressed}}, //comment out
			{11115, new List<string>{"ERROR_IF", Resources.Dic_IfScript}}, //comment out
			{11116, new List<string>{"ERROR_IF", Resources.Dic_IfVehicle}}, //comment out
			{11200, new List<string>{"LOOP", Resources.Dic_Loop}},
			{11300, new List<string>{"BREAK", Resources.Dic_LoopBreak}},
			{11500, new List<string>{"END", Resources.Dic_End}},
			{11700, new List<string>{"EXEC", Resources.Dic_Exec}},
			{11800, new List<string>{"ERROR", Resources.Dic_Label}}, //comment out
			{11900, new List<string>{"ERROR", Resources.Dic_LabelJump}}, //comment out
			{12100, new List<string>{"SWITCH", Resources.Dic_Switch}},
			{12200, new List<string>{"HLVARIABLE", Resources.Dic_HlVariable}},
			{12201, new List<string>{"HLVARIABLE", Resources.Dic_VarRandom}},
			{12202, new List<string>{"HLVARIABLE", Resources.Dic_VarItem}},
			{12203, new List<string>{"HLVARIABLE", Resources.Dic_VarActor}},
			{12204, new List<string>{"HLVARIABLE", Resources.Dic_VarEnemy}},
			{12205, new List<string>{"HLVARIABLE", Resources.Dic_VarChar}},
			{12206, new List<string>{"ERROR", Resources.Dic_VarPartyActorId}},
			{12207, new List<string>{"HLVARIABLE", Resources.Dic_VarGameData}},
			{12208, new List<string>{"ERROR", Resources.Dic_VarScript}}, //comment out
			{12300, new List<string>{"SWITCH", Resources.Dic_SelfSwitch}},
			{12400, new List<string>{"EXEC", Resources.Dic_Timer}},
			{12500, new List<string>{"MONEY", Resources.Dic_Money}},
			{12600, new List<string>{"ITEM", Resources.Dic_Item}},
			{12700, new List<string>{"ITEM", Resources.Dic_Weapon}},
			{12800, new List<string>{"ITEM", Resources.Dic_Armor}},
			{12900, new List<string>{"PARTY", Resources.Dic_Party}},
			{13200, new List<string>{"ERROR", Resources.Dic_BtlBgm}}, //comment out
			{13300, new List<string>{"ERROR", Resources.Dic_WinMe}}, //comment out
			{13400, new List<string>{"SW_SAVE", Resources.Dic_Save}},
			{13500, new List<string>{"SW_MENU", Resources.Dic_Menu}},
			{13600, new List<string>{"SW_ENCOUNTING", Resources.Dic_Encount}},
			{13700, new List<string>{"ERROR", Resources.Dic_PartyOrder}}, //comment out
			{13800, new List<string>{"CHANGE_LAYOUT", Resources.Dic_WindowColor}},
			{13900, new List<string>{"ERROR", Resources.Dic_LostMe}}, //comment out
			{14000, new List<string>{"ERROR", Resources.Dic_VehicleBgm}}, //comment out
			{20100, new List<string>{"PLMOVE", Resources.Dic_PlayerMove}},
			{20200, new List<string>{"ERROR", Resources.Dic_VehiclePosition}}, //comment out
			{20300, new List<string>{"MOVE", Resources.Dic_EventMove}},
			{20400, new List<string>{"CAM_ANIMATION", Resources.Dic_SetCamera}},
			{20500, new List<string>{"MOVEROUTE", Resources.Dic_MoveRoute}},
			{20600, new List<string>{"ERROR", Resources.Dic_VehicleRide}}, //comment out
			{21100, new List<string>{"EVHIDE", Resources.Dic_EventTransparent}},
			{21200, new List<string>{"EFFECT", Resources.Dic_Effect}},
			{21300, new List<string>{"EMOTE", Resources.Dic_Emote}},
			{21400, new List<string>{"DESTROY_EVENT", Resources.Dic_DestroyEvent}},
			{21600, new List<string>{"WALK_IN_ROWS", Resources.Dic_WalkInRows}},
			{21700, new List<string>{"ERROR", Resources.Dic_Gather}}, //comment out
			{22100, new List<string>{"SCREEN_FADE", Resources.Dic_Fadeout}},
			{22200, new List<string>{"SCREEN_FADE", Resources.Dic_Fadein}},
			{22300, new List<string>{"SCREEN_COLOR", Resources.Dic_ScreenColor}},
			{22400, new List<string>{"SCREEN_FLASH", Resources.Dic_Flash}},
			{22500, new List<string>{"SCREEN_SHAKE", Resources.Dic_Shake}},
			{23000, new List<string>{"WAIT", Resources.Dic_Wait}},
			{23100, new List<string>{"SPPICTURE", Resources.Dic_ShowImage}},
			{23200, new List<string>{"SPMOVE", Resources.Dic_MoveImage}},
			{23300, new List<string>{"ERROR", Resources.Dic_RotateImage}}, //comment out
			{23400, new List<string>{"ERROR", Resources.Dic_ChangeImageColor}}, //comment out
			{23500, new List<string>{"SPHIDE", Resources.Dic_DeleteImage}},
			{23600, new List<string>{"CHANGE_RENDER", Resources.Dic_ChangeRender}},
			{24100, new List<string>{"PLAYBGM", Resources.Dic_PlayBgm}},
			{24200, new List<string>{"PLAYBGM", Resources.Dic_FadeoutBgm}},
			{24300, new List<string>{"ERROR", Resources.Dic_SaveBgm}}, //comment out
			{24400, new List<string>{"ERROR", Resources.Dic_ResumeBgm}}, //comment out
			{24500, new List<string>{"PLAYBGS", Resources.Dic_PlayBgs}},
			{24600, new List<string>{"PLAYBGS", Resources.Dic_FadeoutBgs}},
			{24900, new List<string>{"PLAYJINGLE", Resources.Dic_PlayJingle}},
			{25000, new List<string>{"PLAYSE", Resources.Dic_PlaySe}},
			{25100, new List<string>{"STOPSE", Resources.Dic_StopSe}},
			{26100, new List<string>{"PLAYMOVIE", Resources.Dic_PlayMovie}},
			{28100, new List<string>{"ERROR", Resources.Dic_MapName}}, //comment out
			{28200, new List<string>{"ERROR", Resources.Dic_Tileset}}, //comment out
			{28300, new List<string>{"CHANGE_RENDER", Resources.Dic_BtlBack}},
			{28400, new List<string>{"CHANGE_RENDER", Resources.Dic_Back}},
			{28500, new List<string>{"GET_TERRAIN", Resources.Dic_Terrain}},
			{30100, new List<string>{"BOSSBATTLE", Resources.Dic_BossBattle}},
			{30200, new List<string>{"SHOP", Resources.Dic_Shop}},
			{30300, new List<string>{"CHANGE_HERO_NAME", Resources.Dic_HeroName}},
			{31100, new List<string>{"CHG_HPMP", Resources.Dic_Hp}},
			{31200, new List<string>{"CHG_HPMP", Resources.Dic_Mp}},
			{31300, new List<string>{"CHG_STTAILM", Resources.Dic_State}},
			{31400, new List<string>{"FULLRECOV", Resources.Dic_FullRecover}},
			{31500, new List<string>{"CHG_EXP", Resources.Dic_Exp}},
			{31600, new List<string>{"ERROR", Resources.Dic_Level}}, //comment out
			{31700, new List<string>{"STATUS", Resources.Dic_Status}},
			{31800, new List<string>{"CHG_SKILL", Resources.Dic_Skill}},
			{31900, new List<string>{"EQUIP", Resources.Dic_Equip}},
			{32000, new List<string>{"STRING_VARIABLE", Resources.Dic_StringVariable}},
			{32100, new List<string>{"CHANGE_JOB", Resources.Dic_Job}},
			{32200, new List<string>{"PLGRAPHIC", Resources.Dic_CastGraphic}},
			{32300, new List<string>{"ERROR", Resources.Dic_VehicleGraphic}}, //comment out
			{32400, new List<string>{"ERROR", Resources.Dic_Nickname}}, //comment out
			{32500, new List<string>{"ERROR", Resources.Dic_Profile}}, //comment out
			{32600, new List<string>{"ERROR", Resources.Dic_Tp}}, //comment out
			{33100, new List<string>{"BTL_HEAL", Resources.Dic_BtlCastHp}},
			{33200, new List<string>{"BTL_HEAL", Resources.Dic_BtlCastMp}},
			{34200, new List<string>{"ERROR", Resources.Dic_EnemyTp}}, //comment out
			{33300, new List<string>{"BTL_STATUS", Resources.Dic_BtlCastState}},
			{33400, new List<string>{"ERROR", Resources.Dic_EnemyFullRecover}}, //comment out
			{33500, new List<string>{"BTL_APPEAR", Resources.Dic_Emerge}},
			{33600, new List<string>{"ERROR", Resources.Dic_Transform }}, //comment out
			{33700, new List<string>{"EFFECT", Resources.Dic_BtlCastEffect}},
			{33900, new List<string>{"BTL_ACTION", Resources.Dic_BtlCastStatus}},
			{34000, new List<string>{"BTL_STOP", Resources.Dic_BtlEnd}},
			{35100, new List<string>{"SHOW_SCORE_BOARD", Resources.Dic_ShowMenu}},
			{35200, new List<string>{"SAVE", Resources.Dic_ShowSave}},
			{35300, new List<string>{"ERROR", Resources.Dic_GameOver}}, //comment out
			{35400, new List<string>{"ERROR", Resources.Dic_ToTitle}}, //comment out
			{35500, new List<string>{"COMMENT", Resources.Dic_Script}}, //comment out
			{35600, new List<string>{"COMMENT", Resources.Dic_Plugin}}, //comment out
			{35700, new List<string>{"COMMENT", Resources.Dic_PluginCont}}, //comment out
			{40200, new List<string>{"BRANCH", Resources.Dic_Choice}},
			{40300, new List<string>{"BRANCH", Resources.Dic_ChoiceCancel}},
            {40400, new List<string>{"CLOSE", Resources.Dic_ChoicesEnd}},
            {41100, new List<string>{"ELSE", Resources.Dic_Else}},
			{41300, new List<string>{"ENDLOOP", Resources.Dic_EndLoop}},
			{60200, new List<string>{"ELSE", Resources.Dic_IfEscape}},
			{60300, new List<string>{"ELSE", Resources.Dic_IfLost}},

			//route. currently player or this-event only..
			{0100, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveDown}}, //MOVE_DOWN
			{0200, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveLeft}}, //MOVE_LEFT
			{0300, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveRight}}, //MOVE_RIGHT
			{0400, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveUp}}, //MOVE_UP
			{0500, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveLD}}, //MOVE_LOWER_L
			{0600, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveRD}}, //MOVE_LOWER_R
			{0700, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveLU}}, //MOVE_UPPER_L
			{0800, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveRU}}, //MOVE_UPPER_R
			{0900, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_MoveRandom}}, //MOVE_RANDOM
			{1000, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_Toward}}, //MOVE_TOWARD
			{1100, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_Away}}, //MOVE_AWAY
			{1200, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_Forward}}, //MOVE_FORWARD
			{1300, new List<string>{"PLWALK", Resources.Dic_Player + Resources.Dic_Backward}}, //MOVE_BACKWARD
			{1400, new List<string>{"ADDFORCEPL", Resources.Dic_Player + Resources.Dic_Jump}}, //JUMP
			{1500, new List<string>{"WAIT", Resources.Dic_Wait}}, //WAIT
			{1600, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_TurnDown}}, //TURN_DOWN
			{1700, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_TurnLeft}}, //TURN_LEFT
			{1800, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_TurnRight}}, //TURN_RIGHT
			{1900, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_TurnUp}}, //TURN_UP
			{2000, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_RotRight90}}, //TURN_90D_R
			{2100, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_RotLeft90}}, //TURN_90D_L
			{2200, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_Rot180}}, //TURN_180D
			{2300, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_RotLeftRight90}}, //TURN_90D_R_L
			{2400, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_RotRandom}}, //TURN_RANDOM
			{2500, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_TurnToward}}, //TURN_TOWARD
			{2600, new List<string>{"PLROTATE", Resources.Dic_Player + Resources.Dic_TurnAway}}, //TURN_AWAY
			{2700, new List<string>{"SWITCH", Resources.Dic_Player + Resources.Dic_SwitchOn}}, //SWITCH_ON
			{2800, new List<string>{"SWITCH", Resources.Dic_Player + Resources.Dic_SwitchOFF}}, //SWITCH_OFF
			{2900, new List<string>{"PLWALKSPEED", Resources.Dic_Player + Resources.Dic_MoveSpeed}}, //CHANGE_SPEED
			{3000, new List<string>{"ERROR", Resources.Dic_Player + Resources.Dic_MoveFrequency}},	//CHANGE_FREQ
			{3100, new List<string>{"GRAPHIC", Resources.Dic_Player + Resources.Dic_WalkAnimeOn}}, //WALK_ANIME_ON
			{3200, new List<string>{"GRAPHIC", Resources.Dic_Player + Resources.Dic_WaklAnimeOff}}, //WALK_ANIME_OFF
			{3300, new List<string>{"PLMOTION", Resources.Dic_Player + Resources.Dic_StepAnimeOn}}, //STEP_ANIME_ON
			{3400, new List<string>{"PLMOTION", Resources.Dic_Player + Resources.Dic_StepAnimeOff}}, //STEP_ANIME_OFF
			{3500, new List<string>{"SW_PLLOCKROTATE", Resources.Dic_Player + Resources.Dic_DirFixOn}}, //DIR_FIX_ON
			{3600, new List<string>{"SW_PLLOCKROTATE", Resources.Dic_Player + Resources.Dic_DirFixOff}}, //DIR_FIX_OFF
			{3700, new List<string>{"CHANGE_PLAYER_MOVABLE", Resources.Dic_Player + Resources.Dic_ThroughOn}}, //THROUGH_ON
			{3800, new List<string>{"CHANGE_PLAYER_MOVABLE", Resources.Dic_Player + Resources.Dic_ThrougnOff}}, //THROUGH_OFF
			{3900, new List<string>{"PLHIDE", Resources.Dic_Player + Resources.Dic_TransOn}}, //TRANSPARENT_ON
			{4000, new List<string>{"PLHIDE", Resources.Dic_Player + Resources.Dic_TransOff}}, //TRANSPARENT_OFF
			{4100, new List<string>{"GRAPHIC", Resources.Dic_Player + Resources.Dic_CharChip}}, //CHANGE_IMAGE
			{4200, new List<string>{"ERROR", Resources.Dic_Player + Resources.Dic_Opacity}}, //CHANGE_OPACITY
			{4300, new List<string>{"ERROR", Resources.Dic_Player + Resources.Dic_MixChip}}, //CHANGE_BLEND_MODE
			{4400, new List<string>{"PLAYSE", Resources.Dic_Player + Resources.Dic_PlaySe2}}, //PLAY_SE
			{4500, new List<string>{"ERROR", Resources.Dic_Player + Resources.Dic_Script2}}, //SCRIPT
			
			{0101, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveDown}}, //MOVE_DOWN
			{0201, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveLeft}}, //MOVE_LEFT
			{0301, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveRight}}, //MOVE_RIGHT
			{0401, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveUp}}, //MOVE_UP
			{0501, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveLD}}, //MOVE_LOWER_L
			{0601, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveRD}}, //MOVE_LOWER_R
			{0701, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveLU}}, //MOVE_UPPER_L
			{0801, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveRU}}, //MOVE_UPPER_R
			{0901, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_MoveRandom}}, //MOVE_RANDOM
			{1001, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_Toward}}, //MOVE_TOWARD
			{1101, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_Away}}, //MOVE_AWAY
			{1201, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_Forward}}, //MOVE_FORWARD
			{1301, new List<string>{"WALK", Resources.Dic_Event + Resources.Dic_Backward}}, //MOVE_BACKWARD
			{1401, new List<string>{"ADDFORCE", Resources.Dic_Event + Resources.Dic_Jump}}, //JUMP
			{1601, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_TurnDown}}, //TURN_DOWN
			{1701, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_TurnLeft}}, //TURN_LEFT
			{1801, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_TurnRight}}, //TURN_RIGHT
			{1901, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_TurnUp}}, //TURN_UP
			{2001, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_RotRight90}}, //TURN_90D_R
			{2101, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_RotLeft90}}, //TURN_90D_L
			{2201, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_Rot180}}, //TURN_180D
			{2301, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_RotLeftRight90}}, //TURN_90D_R_L
			{2401, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_RotRandom}}, //TURN_RANDOM
			{2501, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_TurnToward}}, //TURN_TOWARD
			{2601, new List<string>{"ROTATE", Resources.Dic_Event + Resources.Dic_TurnAway}}, //TURN_AWAY
			{2701, new List<string>{"SWITCH", Resources.Dic_SwitchOn}}, //SWITCH_ON
			{2801, new List<string>{"SWITCH", Resources.Dic_SwitchOFF}}, //SWITCH_OFF
			{2901, new List<string>{"WALKSPEED", Resources.Dic_Event + Resources.Dic_MoveSpeed}}, //CHANGE_SPEED
			{3001, new List<string>{"ERROR", Resources.Dic_Event + Resources.Dic_MoveFrequency}},	//CHANGE_FREQ
			{3101, new List<string>{"GRAPHIC", Resources.Dic_Event + Resources.Dic_WalkAnimeOn}}, //WALK_ANIME_ON
			{3201, new List<string>{"GRAPHIC", Resources.Dic_Event + Resources.Dic_WaklAnimeOff}}, //WALK_ANIME_OFF
			{3301, new List<string>{"MOTION", Resources.Dic_Event + Resources.Dic_StepAnimeOn}}, //STEP_ANIME_ON
			{3401, new List<string>{"MOTION", Resources.Dic_Event + Resources.Dic_StepAnimeOff}}, //STEP_ANIME_OFF
			{3501, new List<string>{"SW_LOCKROTATE", Resources.Dic_Event + Resources.Dic_DirFixOn}}, //DIR_FIX_ON
			{3601, new List<string>{"SW_LOCKROTATE", Resources.Dic_Event + Resources.Dic_DirFixOff}}, //DIR_FIX_OFF
			{3701, new List<string>{"CHANGE_MOVABLE", Resources.Dic_Event + Resources.Dic_ThroughOn}}, //THROUGH_ON
			{3801, new List<string>{"CHANGE_MOVABLE", Resources.Dic_Event + Resources.Dic_ThrougnOff}}, //THROUGH_OFF
			{3901, new List<string>{"EVHIDE", Resources.Dic_Event + Resources.Dic_TransOn}}, //TRANSPARENT_ON
			{4001, new List<string>{"EVHIDE", Resources.Dic_Event + Resources.Dic_TransOff}}, //TRANSPARENT_OFF
			{4101, new List<string>{"GRAPHIC", Resources.Dic_Event + Resources.Dic_CharChip}}, //CHANGE_IMAGE
			{4201, new List<string>{"ERROR", Resources.Dic_Event + Resources.Dic_Opacity}}, //CHANGE_OPACITY
			{4301, new List<string>{"ERROR", Resources.Dic_Event + Resources.Dic_MixChip}}, //CHANGE_BLEND_MODE

            //commands not used in MV
            {1, new List<string>{"SPTEXT", Resources.Dic_SpText}},
            {3, new List<string>{"CHANGE_GAMEOVER_ACTION", Resources.Dic_ChangeGameoverAction}},
            {4, new List<string>{"WEBBROWSER", Resources.Dic_WebBrowser}},
            {5, new List<string>{"SW_CAMLOCK", Resources.Dic_SwCamLock}},
            {6, new List<string>{"BTL_SW_CAMERA", Resources.Dic_BtlSwCamera}},
            {8, new List<string>{"CHANGE_PLAYER_SCALE", Resources.Dic_ChangePlayerScale}},
            {9, new List<string>{"JOINT_WEAPON", Resources.Dic_JointWeapon}},
            {11, new List<string>{"INVINCIBLE", Resources.Dic_Invincible}},
            {12, new List<string>{"PLSNAP", Resources.Dic_PlSnap}},
            {13, new List<string>{"PLWALK_TGT", Resources.Dic_PlWalkTgt}},
            {14, new List<string>{"ROTATEPL_XYZ", Resources.Dic_RotatePlXyz}},
            {15, new List<string>{"WALK_IN_ROWS_ORDER", Resources.Dic_WalkInRowsOrder}},
            {16, new List<string>{"PLSUBGRP", Resources.Dic_PlSubgrp}},
            {17, new List<string>{"ITEM_THROW_OUT", Resources.Dic_ItemThrowOut}},
            {19, new List<string>{"CHANGE_SCALE", Resources.Dic_ChangeScale}},
            {20, new List<string>{"EVSNAP", Resources.Dic_EvSnap}},
            {21, new List<string>{"EVWALK_TGT", Resources.Dic_EvWalkTgt}},
            {22, new List<string>{"ROTATE_XYZ", Resources.Dic_RotateXyz}},
            {24, new List<string>{"SUBGRP", Resources.Dic_Subgrp}},
            {26, new List<string>{"HLSTRVARIABLE", Resources.Dic_HlStrVariable}},
            {27, new List<string>{"REPLACE_STRING_VARIABLE", Resources.Dic_ReplaceStringVariable}},
            {28, new List<string>{"SW_PLLOCK", Resources.Dic_SwPlLock}},
            {29, new List<string>{"SW_DASH", Resources.Dic_SwDash}},
            {30, new List<string>{"SW_JUMP", Resources.Dic_SwJump}},
            {31, new List<string>{"INN", Resources.Dic_Inn}},
            {32, new List<string>{"SHOT_EVENT", Resources.Dic_ShotEvent}},
            {33, new List<string>{"EXIT", Resources.Dic_Exit}},
            {34, new List<string>{"FACEEMOTION", Resources.Dic_FaceMotion}},
            {35, new List<string>{"IF_INVENTORY_EMPTY", Resources.Dic_IfInventoryEmpty}},
            {36, new List<string>{"BTL_IFBATTLE", Resources.Dic_BtlIfBattle}},
            {37, new List<string>{"COL_CONTACT", Resources.Dic_ColContack}},
            {38, new List<string>{"COL_RAYCAST", Resources.Dic_ColRaycast}},
            {39, new List<string>{"CHANGE_PLAYER_HEIGHT", Resources.Dic_ChangePlayerHeight}},
            {40, new List<string>{"FALL_PLAYER", Resources.Dic_FallPlayer}},
            {41, new List<string>{"CHANGE_HEIGHT", Resources.Dic_ChangeHeight}},
            {42, new List<string>{"FALL_EVENT", Resources.Dic_FallEvent}},
            };

            return codePairs.Where(d => d.Key == code).FirstOrDefault().Value;
        }
        #endregion
    }
}
