﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SharedTools_Stuff;
using PlayerAndEditorGUI;

namespace STD_Logic
{
    
    public class ConditionBranch : AbstractKeepUnrecognized_STD, IGotName , IPEGI {

        public enum ConditionBranchType { OR, AND }

        public List<ConditionLogic> conds = new List<ConditionLogic>();
        public List<ConditionBranch> branches = new List<ConditionBranch>();

        public ConditionBranchType type;
        public string description = "new branch";
        public TaggedTarget targ;
        
        public string NameForPEGI
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public override StdEncoder Encode() =>this.EncodeUnrecognized()
            .Add_ifNotEmpty("wb", branches)
            .Add_ifNotEmpty("v", conds)
            .Add_ifNotZero("t", (int)type)
            .Add_String("d", description)
            .Add("tag", targ)
            .Add_ifNotNegative("insB", browsedBranch)
            .Add("ic", browsedCondition);
        
        public override bool Decode(string subtag, string data)
        {
            switch (subtag)
            {
                case "t": type = (ConditionBranchType)data.ToInt(); break;
                case "d": description = data; break;
                case "tag": data.DecodeInto(out targ); break;
                case "wb": data.DecodeInto(out branches); break;
                case "v": data.DecodeInto(out conds); break;
                case "insB": browsedBranch = data.ToInt(); break;
                case "ic": browsedCondition = data.ToInt(); break;
                default: return false;
            }
            return true;
        }

        public bool TestFor(Values vals) {

            vals = targ.TryGetValues(vals);

            switch (type) {
                case ConditionBranchType.AND:
                    foreach (var c in conds)
                        if (c.TestFor(vals) == false) return false;
                    foreach (var b in branches)
                        if (b.TestFor(vals) == false) return false;
                    return true;
                case ConditionBranchType.OR:
                    foreach (var c in conds)
                        if (c.TestFor(vals) == true)
                            return true;
                    foreach (var b in branches)
                        if (b.TestFor(vals) == true) return true;
                    return ((conds.Count == 0) && (branches.Count == 0));
            }
            return true;
        }
        
        public void ForceToTrue(Values vals)
        {

            vals = targ.TryGetValues(vals);

            switch (type)
            {
                case ConditionBranchType.AND:
                    foreach (var c in conds)
                        c.ForceConditionTrue(vals);
                    foreach (var b in branches)
                        b.ForceToTrue(vals);
                    break;
                case ConditionBranchType.OR:
                    if (conds.Count > 0)
                    {

                        conds[0].ForceConditionTrue(vals);
                        return;
                    }
                    if (branches.Count > 0)
                    {
                        branches[0].ForceToTrue(vals);
                        return;
                    }
                    break;
            }

        }

        public string ToString(Values tell, bool showDetails)
        {
            bool AnyConditions = (conds.Count > 0);

            return TestFor(tell) + " " + (showDetails ? "..." :
                      ((AnyConditions) ? "[" + conds.Count + "]: " + conds[0].ToString() : "UNCONDITIONAL"));
        }

        public string CompileBranchText(Values vals) {

            vals = targ.TryGetValues(vals);

            int br = branches.Count;
            int Conds = conds.Count;

            return type + ":" + TestFor(vals).ToString() + (br > 0 ? br + " br," : " ") + (Conds > 0 ? (conds[0].ToString()) +
                (Conds > 1 ? "+" + (Conds - 1) : "") : "");

        }
        
        int browsedBranch = -1;
        int browsedCondition = -1;

#if !NO_PEGI
        static string path;
        static bool isCalledFromAnotherBranch = false;
        public override bool PEGI() {

            var tmpVals = Values.current;
            Values.current = targ.TryGetValues(tmpVals);

            browsedBranch = Mathf.Min(browsedBranch, branches.Count - 1);

            bool changed = base.PEGI();

            if (!showDebug)
            {

                if (!isCalledFromAnotherBranch)
                    path = "Cnds";
                else
                    path += "->" + NameForPEGI;

                if (browsedBranch == -1)
                {

                    path.nl();

                    if (pegi.Click("Logic: " + type + (type == ConditionBranchType.AND ? " (ALL should be true)" : " (At least one should be true)")
                        + (Values.current != null ? (TestFor(Values.current) ? "True" : "false") : " ")
                        ,
                           (type == ConditionBranchType.AND ? "All conditions and sub branches should be true" :
                            "At least one condition or sub branch should be true")
                            ))
                        type = (type == ConditionBranchType.AND ? ConditionBranchType.OR : ConditionBranchType.AND);

                    conds.edit_List(ref browsedCondition, true);

                    changed |= "Sub Conditions".edit_List(branches, ref browsedBranch, true);

                }
                else
                {
                    isCalledFromAnotherBranch = true;
                    var sub = branches[browsedBranch];
                    if (sub.browsedBranch == -1 && icon.Exit.Click())
                        browsedBranch = -1;
                    else
                        changed |= sub.PEGI();
                    isCalledFromAnotherBranch = false;
                }
            }

            pegi.newLine();

            Values.current = tmpVals;

            return changed;
        }
        
        public void ConditionsFoldout(ref ConditionBranch cond, ref bool Show, string descr)
        {
            bool AnyConditions = ((cond != null) && (cond.conds.Count > 0));
            pegi.foldout(descr + (Show ? "..." :
              ((AnyConditions) ? "[" + cond.conds.Count + "]: " + cond.conds[0].ToString() : "UNCONDITIONAL")), ref Show);

        }
#endif

    }
}