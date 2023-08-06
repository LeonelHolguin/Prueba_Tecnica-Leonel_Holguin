import { FetchBaseURL } from "../constants"


export const sentGameData = async (playerName, playerBalance) => {
    if(!playerName)
        return

    let sentData = new URLSearchParams()
    sentData.append("PlayerName", playerName)
    sentData.append("PlayerBalance", playerBalance)

    
    await fetch(FetchBaseURL + "Player/Save", { method: "post", body: sentData })
}