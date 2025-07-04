
/**
 * 
 * @param {Array} list 
 * @param {String} val 
 * @returns {String}
 * @example 
 */
export function dictFormatter(list, val) {
  return list.find(item => item.value === val).label;
}

// useDict.js
export function useDict(dictList) {
  function getLabel(value) {
    const item = dictList.find(d => d.value === value)
    return item ? item.label : value
  }

  return { getLabel }
}
