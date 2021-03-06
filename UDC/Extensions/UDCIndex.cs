﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UDC.Models;

namespace UDC.Extensions
{
    public class UDCIndex
    {
        public UDCIndex(Int32 count)
        {
            _udcParts = new List<UDCPart>(count);
        }

        public void AddPartIndex(UDCPart partIndex)
        {
            _udcParts.Add(partIndex);
        }

        public void AddUDCInDataBase()
        {
            _db.ExecuteCommand("DELETE FROM dbo.CurrentIndex");
            _db.SubmitChanges();
            if (_udcParts.Count > 1)
            {
                for (int i = 0; i < _udcParts.Count; i++)
                {
                    _db.ExecuteCommand("INSERT INTO dbo.CurrentIndex VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, '', {7}, '[', ']')",
                        i + 1,
                        _udcParts[i].GetMainIndex(),
                        _udcParts[i].GetLanguageIndex(),
                        _udcParts[i].GetFormIndex(),
                        _udcParts[i].GetTimeIndex(),
                        _udcParts[i].GetPlaceIndex(),
                        _udcParts[i].GetNationIndex(),
                        _udcParts[i].GetSignBetween());
                    _db.SubmitChanges();
                }
            }
            else
            {
                _db.ExecuteCommand("INSERT INTO dbo.CurrentIndex VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, '', {7},  '', '')",
                        1,
                        _udcParts[0].GetMainIndex(),
                        _udcParts[0].GetLanguageIndex(),
                        _udcParts[0].GetFormIndex(),
                        _udcParts[0].GetTimeIndex(),
                        _udcParts[0].GetPlaceIndex(),
                        _udcParts[0].GetNationIndex(),
                        _udcParts[0].GetSignBetween());
                _db.SubmitChanges();
            }
        }

        public void Parse()
        {
            for (int i = 0; i < _udcParts.Count; i++)
            {
                _udcParts[i].MainIndexParse();
                _udcParts[i].LanguageIndexParse();
                _udcParts[i].PlaceIndexParse();
                _udcParts[i].FormIndexParse();
                _udcParts[i].TimeIndexParse();
                _udcParts[i].NationIndexParse();
            }
        }

        public List<UDCPart> GetUdcParts()
        {
            return _udcParts;
        }

        private CurrentIndexLtSDataContext _db = new CurrentIndexLtSDataContext();
        private List<UDCPart> _udcParts;
    }
}