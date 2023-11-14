
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
