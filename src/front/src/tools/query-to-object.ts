export function queryToObject(query: string): any {
  const parameters = new URLSearchParams(query);
  return Object.fromEntries(parameters.entries());
}
