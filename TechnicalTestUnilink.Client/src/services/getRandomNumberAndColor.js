import { FetchBaseURL } from "../constants.js"

export const getRandomNumberAndColor = async () => {
    const res = await fetch(FetchBaseURL + "Roulette/GetNumberAndColor")
    const data = await res.json()

    return data
}