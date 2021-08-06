<script>

	let buttons = [0, 0, 0, 0, 0, 0, 0, 0, 0];
	let player = 1;
	

	function switchPlayer() {
		if (player === 1) {
			player = 2;
		} else {
			player = 1;
		}

		// player = player === 1 ? 2 : 1;
	}

	function handleClick(buttonIndex) {
		if (buttons[buttonIndex] == 0) {
			buttons[buttonIndex] = player;
			if (checkWin() == false) {
                console.log("test")
				switchPlayer();
			}
		} else {
			window.alert('The Button was already pressed!');
			checkWin();
		}
	}

	function checkWin() {
		if (
			(buttons[0] == player && buttons[1] == player && buttons[2] == player) ||
			(buttons[3] == player && buttons[4] == player && buttons[5] == player) ||
			(buttons[6] == player && buttons[7] == player && buttons[8] == player) ||
			(buttons[0] == player && buttons[3] == player && buttons[6] == player) ||
			(buttons[1] == player && buttons[4] == player && buttons[7] == player) ||
			(buttons[2] == player && buttons[5] == player && buttons[8] == player) ||
			(buttons[0] == player && buttons[4] == player && buttons[8] == player) ||
			(buttons[2] == player && buttons[4] == player && buttons[6] == player)
		) {
			if (player == 1) {
				window.alert('Player 1 has won the Game!');
			} else if (player == 2) {
				window.alert('Player 2 has won the Game!');
			}
			buttons = [0, 0, 0, 0, 0, 0, 0, 0, 0];
			player = 1;
			return true;
		} else {
			let b = 0;
			for (let x = 0; x < buttons.length; x++) {
				if (buttons[x] != 0) {
					b++;
				}
			}
			if (b == 9) {
				window.alert('It´s a draw!');
				buttons = [0, 0, 0, 0, 0, 0, 0, 0, 0];
				player = 1;
				return true;
			}
		}
		return false;
	}

	/*
        if (
			buttons[0] != 0 &&
			buttons[1] != 0 &&
			buttons[2] != 0 &&
			buttons[3] != 0 &&
			buttons[4] != 0 &&
			buttons[5] != 0 &&
			buttons[6] != 0 &&
			buttons[7] != 0 &&
			buttons[8] != 0
		) {
			window.alert('It´s a draw!');
			buttons = [0, 0, 0, 0, 0, 0, 0, 0, 0];
			player = 1;
			return true;
		} else {
			return false;
		}*/
</script>

<ul>
	{#each buttons as button, i}
		<li>
			<button
				class="buttons"
				on:click={() => {
					handleClick(i);
				}}>{button}</button
			>
		</li>
	{/each}
</ul>

Player: {player}

<style>
	ul {
		list-style-type: none;
		padding-left: 0;
		display: grid;
		gap: 30px;
		grid-template-columns: repeat(3, 200px);
		grid-template-rows: repeat(3, 200px);
		place-items: stretch;
	}
	li {
		display: contents;
	}
	.buttons {
		background-color: rgb(9, 255, 0);
		border: solid 2px black;
		color: black;
		text-align: center;
		text-decoration: none;
		cursor: pointer;
		display: block;
	}
	.buttons:hover{
		background-color: rgb(9, 255, 0);
	}
</style>
