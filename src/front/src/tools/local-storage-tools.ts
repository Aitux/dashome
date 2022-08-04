export const getObject = (key: string): any => {
  const value = localStorage.getItem(key)
  return value ? JSON.parse(value) : null
}
