﻿using DatReaderWriter;
using DatReaderWriter.Options;
using DatReaderWriter.Lib.Attributes;
using DatReaderWriter.Lib.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatReaderWriter.Lib {
    /// <summary>
    /// A queryable collection of <see cref="IDBObj"/>s
    /// </summary>
    /// <typeparam name="T">The type of DBObjs in the collection</typeparam>
    public class DBObjCollection<T> : IEnumerable<T> where T : IDBObj {
        private readonly DBObjTypeAttribute _typeAttr;
        private readonly DatDatabase _dat;

        /// <summary>
        /// Creates a new DBObjCollection
        /// </summary>
        /// <param name="dat"></param>
        /// <exception cref="Exception"></exception>
        public DBObjCollection(DatDatabase dat) {
            _dat = dat;

            if (!DBObjAttributeCache.TypeCache.TryGetValue(typeof(T), out var typeAttr)) {
                throw new Exception($"Type {typeof(T).FullName} not registered in DBObjAttributeCache. Ensure it has a DBObjTypeAttribute attached.");
            }

            _typeAttr = typeAttr;
        }

        /// <summary>
        /// Get an enumerator that yields all <see cref="IDBObj"/> entries
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator<T> GetEnumerator() {
            if (_typeAttr.HasRangeData || _typeAttr.IsSingular) {
                foreach (var entry in _dat.Tree.GetFilesInRange(_typeAttr.FirstId, _typeAttr.LastId)) {
                    if (_dat.TryReadFile<T>(entry.Id, out var dbObj)) {
                        yield return dbObj;
                    }
                }
            }
            else {
                foreach (var entry in _dat.Tree) {
                    if (_dat.TypeFromId(entry.Id) == _typeAttr.DBObjType && _dat.TryReadFile<T>(entry.Id, out var dbObj)) {
                        yield return dbObj;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
