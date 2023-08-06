import { FetchBaseURL } from "../constants"


export const getPrize = async (gameDetails) => {
    let sentData = new URLSearchParams()
    sentData.append("win", gameDetails.win)
    sentData.append("betType", gameDetails.betType)
    sentData.append("betAmount",gameDetails.betAmount)

    const res = await fetch(FetchBaseURL + "Roulette/Prize", { method: "post", body: sentData })
    const data = await res.json()

    return data.prize
}