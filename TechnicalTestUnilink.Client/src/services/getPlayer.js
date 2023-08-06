import { FetchBaseURL } from "../constants.js"

export const getPlayer = async (playerId) => {
    const res = await fetch(FetchBaseURL + `Player/Get/${playerId}`)
    const data = await res.json()

    return data
}