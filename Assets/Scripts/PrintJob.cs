public class PrintJob {
	public string name;
	public int paper;
	public int cyan;
	public int magenta;
	public int yellow;
	public int black;
	public int moralityModifier;
	public int satisfactionModifier;
	public int timeToComplete;

	public PrintJob(string name, int paper, int cyan, int magenta, int yellow, int black, int moralityModifier,
		int satisfactionModifier, int timeToComplete) {
		this.name = name;
		this.paper = paper;
		this.cyan = cyan;
		this.magenta = magenta;
		this.yellow = yellow;
		this.black = black;
		this.moralityModifier = moralityModifier;
		this.satisfactionModifier = satisfactionModifier;
		this.timeToComplete = timeToComplete;
	}

	public bool IsBlackAndWhite() {
		return cyan == 0 && magenta == 0 && yellow == 0;
	}
}