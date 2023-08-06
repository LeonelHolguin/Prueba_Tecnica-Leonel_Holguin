<script setup>
    import { getPlayer, getRandomNumberAndColor, getPrize, sentGameData } from "../services";
    import { ref } from "vue";

    let betType = ref("")
    let playerBalance = ref(0)
    let betColor = ref("")
    let betIsEven = ref(null)
    let betNumber = ref(0)
    let playerName = ref("")
    let betAmount = ref(0)
    let isGameStarted = ref(false)
    let blockBalance = ref(false)

    const betTypes = [
        {value: "color", text: "1.Por color"},
        {value: "evenOddForColor", text: "2.Por par e impar de un color"},
        {value: "numberAndColor", text: "3.Por numero y color"}
    ]

    const colors = {
        black: "negro",
        red: "rojo"
    }

    let playerDetails = {}

    let rouletteDetails = {}

    let roundDetails = {}

    const startGame = async () => {
        if(playerBalance.value <=  0 || betAmount.value > playerBalance.value) 
            return
        
        isGameStarted.value = true
        blockBalance.value = true


        let setBetNumber
        if (betNumber.value > 36) 
            setBetNumber = 36
        else if (betNumber.value < 0)
            setBetNumber = 0
        else
            setBetNumber = betNumber.value

        playerDetails.name = playerName.value
        playerDetails.balance = playerBalance.value
        playerDetails.betType = betType.value
        playerDetails.color = betColor.value
        playerDetails.number = setBetNumber
        playerDetails.betAmount = betAmount.value
        playerDetails.isEven = betIsEven.value

        const { color, number } = await getRandomNumberAndColor()
        const isEven = number % 2 === 0
        rouletteDetails = { color, number, isEven }
        const gameDetails = getRoundResult(rouletteDetails, playerDetails)
        const prizeGained = await getPrize(gameDetails)

        if (prizeGained >= 1)
            playerBalance.value += prizeGained
        else
            playerBalance.value -= playerDetails.betAmount

        roundDetails = { isWinner: gameDetails.win, prizeGained, color: colors[color], number, isEven }
        
        roundDetails.isWinner ? playerDetails.balance += roundDetails.prizeGained : playerDetails.balance -= playerDetails.betAmount
    }

    const getRoundResult = (roulette, player) => {
        let isWinner
        
        switch(player.betType) {
            case "color":
                isWinner = roulette.color === player.color
                break;
            case "evenOddForColor":
                isWinner = roulette.color === player.color && roulette.isEven === player.isEven
                break;
            case "numberAndColor":
                isWinner = roulette.color === player.color && roulette.number === player.number
                break;
        }

        return { win: isWinner, betType: player.betType, betAmount: player.betAmount }
    }


    const saveGame = async (playerName, playerBalance) => {

            await sentGameData(playerName, playerBalance)
            isGameStarted.value = false
            blockBalance.value = false
    }   

    const loadBalance = async (playerName) => {
        try {
            const { balance } = await getPlayer(playerName)

            playerBalance.value = balance
            blockBalance.value = true

        } catch(error) {
            playerBalance.value = 0
            blockBalance.value = false
        }
    }

</script>

<template>
    <header>
        <nav class="nav">
            <h2 class="navTitle">Saldo</h2>

            <section class="navContent">
                <label for="name">Ingrese su nombre
                    <input type="text" name="name" v-model="playerName"/>
                </label>

                <label for="initialBalance">Balance
                <input type="number" name="initialBalance" v-model="playerBalance" :disabled="isGameStarted || blockBalance"/>  
                </label>

                <span @click="loadBalance(playerName)" class="material-icons btnRefresh">refresh</span>
            </section>
        </nav>
    </header>

    <main>
        <section>
            <h1>Juego de la ruleta</h1>
            <h4>Seleccione su apuesta</h4>
        </section>
    
    
        <section class="mainContent">
            <h3>Apuesta</h3>
    
            <div>
                <label for="number">Ingrese la cantidad de dinero a apostar</label>
                <input type="number" name="number" min="0" :max="playerBalance" v-model="betAmount"/>
            </div>
    
            <label for="betType">Seleccione el tipo de apuesta</label>
            <select name="betType" v-model="betType">
                <option v-for="item in betTypes" :key="item.value" :value="item.value">
                {{ item.text }}
                </option>
            </select>
    
            <div>
                <input type="radio" name="color-red" v-model="betColor" value="red" />
                <label for="color-red">Rojo</label>
    
                <input type="radio" name="color-black" v-model="betColor" value="black" />
                <label for="color-black">Negro</label>
            </div>
    
            <div v-if="betType === 'evenOddForColor'">
                <input type="radio" name="even" v-model="betIsEven" :value="true"/>
                <label for="even">Par</label>
    
                <input type="radio" name="odd" v-model="betIsEven" :value="false"/>
                <label for="odd">Impar</label>
            </div>
    
            <div v-if="betType === 'numberAndColor'">
                <label for="number">Seleccione su numero a apostar</label>
                <input type="number" name="number" min="0" max="36" v-model="betNumber"/>
            </div>
        </section>
        
        <section v-if="isGameStarted">
            <h2 v-if="roundDetails.isWinner">usted ha ganado!!!</h2>
            <h2 v-else>usted ha perdido :c</h2>
    
            <h3>Detalles de la partida</h3>
            <p>
                Numero obtenido: {{ roundDetails.number }} / 
                <span v-if="roundDetails.isEven">PAR</span> 
                <span v-else>IMPAR</span>  
            </p>
            <p>Color obtenido: {{ roundDetails.color }}</p>
        </section>
    
        <button @click="startGame">Iniciar juego</button>
        <button @click="saveGame(playerName, playerBalance)">Guardar juego</button>
    </main>
</template>


<style scoped>
main {
    display: grid;
    place-content: center;
}

.nav {
    display: flex;
    justify-content: space-between;
}

.navContent {
    display: flex;
    align-items: center;
}

.nav .navContent .btnRefresh {
    cursor: pointer;
    border-radius: 50%;
}

.nav .navContent .btnRefresh:hover {
    background-color: #f5f5f5;
}
</style>