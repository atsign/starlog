<script lang="ts">
    import {
        Button,
        ButtonGroup,
        Card,
        CardHeader,
        CardSubtitle,
        CardTitle,
        CardBody,
        CardText,
        CardFooter,
        Icon
    } from 'sveltestrap';
    
    import type { ObservationModel } from './models/ObservationModel';

    const timeFormatter = new Intl.DateTimeFormat('en', {
        hour12: true,
        hour: 'numeric',
        minute: '2-digit'
    });

    export let observation: ObservationModel;
</script>

<Card style="margin-bottom: 54px;">
    <CardHeader>
        <CardTitle>
            <time>{timeFormatter.format(observation.dateTime)}</time>
            {observation.celestialObject.name}
        </CardTitle>
    </CardHeader>
    <CardBody>
        <CardSubtitle>
            <div class="text-muted">
                <strong>R.A.</strong> {observation.celestialObject.rightAscension}
                <br/>
                <strong>Dec.</strong> {observation.celestialObject.declination}<br/>
            </div>
        </CardSubtitle>
        <CardText style="margin-top: 20px;">
            <p>{observation.notes}</p>
        </CardText>
    </CardBody>
    <CardFooter>
        <ButtonGroup>
            <Button color="primary" size="sm" outline>
                <Icon name="pencil-square" />
            </Button>
            <Button color="danger" size="sm" outline>
                <Icon name="trash2" />
            </Button>
        </ButtonGroup>
    </CardFooter>
</Card>

<style>
    time {
        font-weight: 100;
    }
</style>