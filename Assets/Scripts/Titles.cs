using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public class AllTitles
{
    public static readonly IncomingTitle[] Titles =
    {
    new IncomingTitle(
        "b/data",
        "Roddent listed as",
        new EditableTitleComponent(
            new EditableComponentChoice("lowest",-2, -1, 2, 1),
            new EditableComponentChoice("highest", 2, 3, -2, -1)
        ),
        "reliability news site, survey says."
        ),
    new IncomingTitle(
        "b/data",
        "Earthquake ruptures",
        new EditableTitleComponent(
            new EditableComponentChoice("Roddent", -2, -2, 2, 2),
            new EditableComponentChoice("Fancybook", 2, 2, -2, -2)
        ),
        "datacentre. Costs projected to be in the millions!"
        ),
    new IncomingTitle(
        "b/stocks",
        "Fancybook stocks",
        new EditableTitleComponent(
            new EditableComponentChoice("gaining", -2, -2, 2, 2),
            new EditableComponentChoice("neutral", 1, 1, -2, -2),
            new EditableComponentChoice("falling", 2, 2, -2, -2)
        ),
        "after new feature released showing how trustworthy a post is. Experts question the new trustworthiness system's trustworthiness."
        ),
    new IncomingTitle(
        "b/stocks",
        "Market crash leaves",
        new EditableTitleComponent(
            new EditableComponentChoice("Fancybook", -1, -1, 2, 2),
            new EditableComponentChoice("Roddent", 2, 2, -1, -1)
        ),
        "stocks relatively unaffected. Warner Buffey advises investors to \"Take this as an opportunity to buy high, sell low.\""
        ),
    new IncomingTitle(
        "b/cooking",
        "I cooked an egg in the shape of",
        new EditableTitleComponent(
            new EditableComponentChoice("a dog!", 4, -1, 0, 0),
            new EditableComponentChoice("a flower!", 4, 3, 0, 0),
            new EditableComponentChoice("an egg.", 1, 2,0,0)
        )
        ),
    new IncomingTitle(
        "b/cooking",
        "Here's my triple layer",
        new EditableTitleComponent(
                new EditableComponentChoice("toastie", 2, -1, 0, 0),
                new EditableComponentChoice("cheesecake", 4, 3, 0, 0)
            ),
        "I made for breakfast today."
        ),
    new IncomingTitle(
        "b/conspiracy",
        "I think",
        new EditableTitleComponent(
                new EditableComponentChoice("Roddent", -2, -2, 2, -2),
                new EditableComponentChoice("Fancybook", 5, 5, -5, 5)
            ),
        "are editing post titles without our knowledge! What do you think?"
        ),
    new IncomingTitle(
        "b/conspiracy",
        "I don't trust",
        new EditableTitleComponent(
                new EditableComponentChoice("Roddent,", -2, -2, 2, -2),
                new EditableComponentChoice("Fancybook,", 5, 5, -5, 5)
            ),
        "it seems like posts are being edited shortly after they are posted.",
        new EditableTitleComponent(
                new EditableComponentChoice("Upvote", -2, -4, 0, 0),
                new EditableComponentChoice("Downvote", 4, 6, 0, 0)
            ),
        "this post if you think so too!"
        ),
    new IncomingTitle(
        "b/jobs",
        new EditableTitleComponent(
            new EditableComponentChoice("highest", -2, -3, 0, 0),
            new EditableComponentChoice("lowest", 2, 1, 0, 0)
        ),
        "unemployment in recorded history right now. How are you faring?"
        ),
    new IncomingTitle(
        "b/jobs",
        "I just got a call from",
        new EditableTitleComponent(
                new EditableComponentChoice("Fancybook", -4, -1, 5, 1),
                new EditableComponentChoice("Roddent", 8, 6, -2, -1)
            ),
        "today, and I was offered much more than I expected. Guess who is starting their new job tomorrow!"
        ),
    new IncomingTitle(
        "b/games",
        "Looking forward to the release of",
        new EditableTitleComponent(
                new EditableComponentChoice("Just Because 5.", 2, -1, 0, 0),
                new EditableComponentChoice("The Simulations 5.", 2, 1, 0, 0),
                new EditableComponentChoice("CyberPink 2088.", 4, 2, 2, 0)
            ),
        "I hope it doesn't get",
         new EditableTitleComponent(
                new EditableComponentChoice("delayed", 2, -1, 0, 0),
                new EditableComponentChoice("cancelled", 5, 4, 0, 0)
                ),
         "again!"
        ),
    new IncomingTitle(
        "b/games",
        "You can now link your Y-Box account with",
        new EditableTitleComponent(
                new EditableComponentChoice("Fancybook", -4, -1, 5, 1),
                new EditableComponentChoice("Roddent", 8, 6, -2, -1)
            )
        ),
    new IncomingTitle(
        "b/celeb",
        "Michaela Jordana wins World Golf Tournament. Biggest sponsor Fancybook looking to",
            new EditableTitleComponent(
                new EditableComponentChoice("extend", 2, -1, 5, -1),
                new EditableComponentChoice("cancel", 5, 4, -8, -1)
                ),
            "contract \"immediately\"."),
    new IncomingTitle(
        "b/celeb",
        "Markus Zacharyberg offers to purchase",
        new EditableTitleComponent(
                new EditableComponentChoice("Fancybook", -4, -1, 5, 3),
                new EditableComponentChoice("Roddent", 6, 6, -2, -1)
            ),
        "for $1 million, says \"I can take out a small loan\""
        ),
    new IncomingTitle(
       "b/law_and_order",
       "Movie celeb charged with",
       new EditableTitleComponent(
                new EditableComponentChoice("littering,", 1, -1, 0, 0),
                new EditableComponentChoice("tax fraud,", 6, 6, 0, 0)
            ),
       "jury finds",
        new EditableTitleComponent(
                new EditableComponentChoice("innocent.", 2, -2, 0, 0),
                new EditableComponentChoice("guilty.", 3, 3, 0, 0)
            )
       ),
    new IncomingTitle(
        "b/law_and_order",
        "Should we ban use of",
        new EditableTitleComponent(
                new EditableComponentChoice("hats", 1, -1, 0, 0),
                new EditableComponentChoice("tasers", 3, 3, 0, 0)
            ),
        "in police training? Chief says \"It would hinder our cadets' experience.\""
        ),
    new IncomingTitle(
        "b/cool",
        "I found a",
        new EditableTitleComponent(
                new EditableComponentChoice("nice", 1, -1, 0, 0),
                new EditableComponentChoice("crazy", 3, 5, 0, 0)
            ),
        "rock in my yard, take a look!"
        ),
    new IncomingTitle(
        "b/cool",
        "My mother passed away some time ago, and I just found this old",
        new EditableTitleComponent(
                new EditableComponentChoice("stopwatch", 1, -1, 0, 0),
                new EditableComponentChoice("gold bar", 4, 7, 0, 0)
            ),
        "in her drawer. I wonder where she got it..."
        ),
    new IncomingTitle(
        "b/healthy",
         new EditableTitleComponent(
                new EditableComponentChoice("Masks", 1, -1, 0, 0),
                new EditableComponentChoice("Social Media", 3, 5, 0, 0)
            ),
         "should be mandatory to prolong life, health officials say."
        ),
    new IncomingTitle(
        "b/healthy",
        "Data suggests",
        new EditableTitleComponent(
                new EditableComponentChoice("social media", -2, -2, -5, 2),
                new EditableComponentChoice("driving", 3, 5, 0, 0)
            ),
        "could be doing more harm than good."
        ),
    new IncomingTitle(
        "b/family_life",
        "How can I make my children's holiday more safe and enjoyable? I have seen them on",
        new EditableTitleComponent(
                new EditableComponentChoice("Roddent,", -2, -2, 1, -1),
                new EditableComponentChoice("Fancybook,", -4, 2, -1, 5)
            ),
        "too much and I don't think it's good for them."
        ),
    new IncomingTitle(
        "b/family_life",
        "My dad won't talk to me, I think he is addicted to",
        new EditableTitleComponent(
                new EditableComponentChoice("Roddent!", -2, -2, 1, -1),
                new EditableComponentChoice("Fancybook!", 3, 2, -2, 5)
            )
        ),
    new IncomingTitle(
        "b/0111000001100011",
        "Leaked documents show",
        new EditableTitleComponent(
                new EditableComponentChoice("Roddent", -2, -1, 1, -1),
                new EditableComponentChoice("Fancybook", 3, 2, -2, 5)
            ),
        "users can be tracked by law enforcement officers across the web by use of \"Biscuits\""
        ),
    new IncomingTitle(
        "b/0111000001100011",
        "Leaked CPU design from EMD suggests",
         new EditableTitleComponent(
                new EditableComponentChoice("double layer", 2, -1, 0, 0),
                new EditableComponentChoice("quantum", 5, 4, 0, 0)
            ),
         "transistors will be coming to mainstream chips soon."
        ),
    new IncomingTitle(
        "b/maths",
        "My professor just proved that 1 + 1 =",
        new EditableTitleComponent(
                new EditableComponentChoice("2", 2, -1, 0, 0),
                new EditableComponentChoice("3", 5, 4, 0, 0)
            ),
        "and I need to reevaluate my life choices."
        ),
    new IncomingTitle(
        "b/maths",
        "Thought-to-be unsolvable equation just solved by",
        new EditableTitleComponent(
                new EditableComponentChoice("senior", 2, -2, 0, 0),
                new EditableComponentChoice("junior", 5, 4, 0, 0)
            ),
        "mathematician."
        ),
    new IncomingTitle(
        "b/memes_and_dreams",
        "Internet historian suggest Popo the Toad meme originated from",
         new EditableTitleComponent(
                new EditableComponentChoice("Fancybook", -4, -1, 5, 3),
                new EditableComponentChoice("Roddent", 6, 6, -2, -1)
            ),
         "over 15 years ago, and was drawn by cartoonist Matton Fury."
        ),
    new IncomingTitle(
        "b/memes_and_dreams",
        "I found this app called TikTak, and it's a meme",
        new EditableTitleComponent(
                new EditableComponentChoice("desert.", -1, -1, 0, 0),
                new EditableComponentChoice("goldmine!", 3, 6, 0, 0)
            )
        )
    };
}

