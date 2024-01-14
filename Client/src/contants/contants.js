const url = import.meta.env.VITE_BASE_URL
const port = import.meta.env.VITE_BASE_URL_PORT
export const baseUrl = port ? `${url}:${port}` : `${url}`