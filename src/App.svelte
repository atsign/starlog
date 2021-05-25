<script lang="typescript">
	import { Col, Container, Row, Toast, ToastHeader, ToastBody } from 'sveltestrap';

	async function getRandomStar(): Promise<string> {
			const response = await fetch('/api/message');

			if (response.ok) {
				return JSON.parse(await response.text()).star;
			} else {
				console.log(await response.text())
				throw new Error();
			}
	}

	const promise = getRandomStar();
</script>
	
<svelte:head>
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css">
</svelte:head>

<Container>
	<Row>
		<Col sm={{ size: 8, offset: 2 }}>
			<h1>StarLog</h1>

			{#await promise}
				<p>Fetching your random star...</p>
			{:then starName} 
				<Toast>
					<ToastHeader>Clear Skies! :)</ToastHeader>
					<ToastBody>
						Your random star is {starName}
					</ToastBody>
				</Toast>
			{:catch err}
				<Toast>
					<ToastHeader>Cloudy Skies! :(</ToastHeader>
					<ToastBody>There was a problem fetching your random star.</ToastBody>
				</Toast>
			{/await}
		</Col>
	</Row>
</Container>
