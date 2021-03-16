using CSNXML.Models;
using CSNXML.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace CSNXML
{
    public static class CSNFileFactory
    {

        public static CSNModels.StuderanderapportTyp CSNStuderanderapport(int SchoolId, List<StudyPeriod> studyPeriods)
        {
            var csnStuderanderapport = new CSNModels.StuderanderapportTyp();
            var csnSchool = new CSNModels.SkolaTyp()
            {
                skolId = SchoolId
            };

            var csnStuderandeLista = new List<CSNModels.StuderandeTyp>();

            var swedishSocialSecurityNumbers = studyPeriods.Select(sp => new
            {
                SwedishSocialSecurityNumber = sp.SwedishSocialSecurityNumber
            }).Distinct();

            foreach (var swedishSocialSecurityNumber in swedishSocialSecurityNumbers)
            {
                var csnStuderande = new CSNModels.StuderandeTyp()
                {
                    personnummer = swedishSocialSecurityNumber.SwedishSocialSecurityNumber.Replace("-", "")
                };

                var csnStudieperioder = new List<CSNModels.StudieperiodTyp>();

                var studentStudyPeriods = studyPeriods.Where(sp => sp.SwedishSocialSecurityNumber == swedishSocialSecurityNumber.SwedishSocialSecurityNumber);

                foreach (var studyPeriod in studentStudyPeriods)
                {
                    var studieperiod = new CSNModels.StudieperiodTyp();
                    studieperiod.fromDatum = studyPeriod.StartDate;
                    studieperiod.tomDatum = studyPeriod.EndDate;
                    studieperiod.omfattning = studyPeriod.Scope;

                    var changedDate = studyPeriod.ChangeDate;
                    if (changedDate == null)
                    {
                        studieperiod.andringsDatumSpecified = false;
                    }
                    else
                    {
                        studieperiod.andringsDatumSpecified = true;
                        studieperiod.andringsDatum = (DateTime)changedDate;
                    }


                    studieperiod.resultatAvbrott = studyPeriod.Termination == true ? "J" : (studyPeriod.Termination == false ? "N" : null);
                    var terminatedDate = studyPeriod.TerminationDate;
                    if (terminatedDate == null)
                    {
                        studieperiod.avbrottsDatumSpecified = false;
                    }
                    else
                    {
                        studieperiod.avbrottsDatumSpecified = true;
                        studieperiod.avbrottsDatum = (DateTime)terminatedDate;
                    }

                    studieperiod.resultat = studyPeriod.Result == true ? "J" : (studyPeriod.Result == false ? "N" : null);
                    var resultDate = studyPeriod.ResultDate;
                    if (resultDate == null)
                    {
                        studieperiod.resultatDatumSpecified = false;
                    }
                    else
                    {
                        studieperiod.resultatDatumSpecified = true;
                        studieperiod.resultatDatum = (DateTime)resultDate;
                    }

                    studieperiod.uppdragsutbildning = studyPeriod.CommissionedEducation == true ? "J" : (studyPeriod.CommissionedEducation == false ? "N" : null);

                    csnStudieperioder.Add(studieperiod);
                }
                csnStuderande.studieperiod = csnStudieperioder.ToArray();
                csnStuderandeLista.Add(csnStuderande);
            }

            csnSchool.studerande = csnStuderandeLista.ToArray();

            csnStuderanderapport.skola = csnSchool;

            return csnStuderanderapport;
        }
        public static string CSNStuderanderapportXmlDocumentString(CSNModels.StuderanderapportTyp csnStuderanderapport)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CSNModels.StuderanderapportTyp));
            string serializedData = null;

            using (var sw = new Utf8StringWriter())
            {
                serializer.Serialize(sw, csnStuderanderapport);
                serializedData = sw.ToString();
            }

            return serializedData;
        }
    }
}
